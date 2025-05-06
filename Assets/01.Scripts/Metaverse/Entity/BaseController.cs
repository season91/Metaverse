using UnityEngine;

// Metaverse - 연결은 안하고, Player와 Enemy에서 공통으로 사용하는 로직 구현
public class BaseController : MonoBehaviour
{
    // [화면에 실제로 움직여지게 할 요소]
    // 1. 캐릭터 실제 이동 처리를 위한 호출
    private Rigidbody2D _rigidbody;
    // 2. 캐릭터
    [SerializeField] private SpriteRenderer characterRenderer;
    // 3. 캐릭터 무기
    [SerializeField] private Transform weaponPivot;


    // [캐릭터 움직임에 필요한 변수]
    // 1. 이동하는 방향 지정 Move
    protected Vector2 movementDirection = Vector2.zero; // 상속받는 곳에서 사용해야하므로 protected
    public Vector2 MovementDirection { get { return movementDirection; } }
    // 2. 바라보는 방향 지정 Look
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }


    // 3. 무기-활-원거리 공격을 위한 호출
    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;
    protected bool isAttacking; // InputSystem에서 value set
    
    private float timeSinceLastAttack = float.MaxValue;
    // 넉백에 대한 방향
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;


    // 4. 스탯 사용을 위한 호출
    protected StatHandler statHandler;

    // 5. 애니메이션사용을 위한 호출
    protected AnimationHandler animationHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();

        // weaponPivot에 무기 WeaponPrefab 복제 생성
        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            // 이미 무기 장착하고 있다면 가져오기
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    private void Update()
    {
        // lookDirection 값은 InputSystem OnLook에서 설정
        Rotate(lookDirection);
        HandleAction();
        HandleAttackDelay();  // 일정 시간마다 발사할 수 있게 해주겠다는 의미
    }

    private void FixedUpdate()
    {
        // movementDirection값은 InputSystem OnMove에서 설정
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {
        // enemy에서 사용함
    }

    // 이동에 대한 처리->물리 연산으로 FixedUpdate에서 호출
    private void Movment(Vector2 direction)
    {
        // statHandler의 속도
        direction = direction * statHandler.Speed;

        // 넉백처리
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    // 바라보는 방향으로 캐릭터 회전 처리
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        // 무기 회전
        weaponHandler?.Rotate(isLeft);
    }

    // 공격
    // 공격 딜레이 실행
    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    // Attack을 위 딜레이로 호출할거임
    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

    // 넉백을 얼마나, 얼만큼 적용할 것인지
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    // 사망 처리
    public virtual void Death()
    {
        // 움직임 중지
        _rigidbody.velocity = Vector3.zero;

        // 하위 모든 스프라이트 찾아와서 연출
        // 모든 SpriteRenderer의 투명도 낮춰서 죽은 효과 연출
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        //  모든 컴포넌트(스크립트 포함) 비활성화
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // 2초있다 오브젝트 파괴
        Destroy(gameObject, 2f);
    }
}
