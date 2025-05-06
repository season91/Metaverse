using UnityEngine;

// Metaverse - Arrow 프리팹 연결
// 원거리 무기 발사체 관리 - 초기화, 충돌처리, 파괴
public class ProjectileController : MonoBehaviour
{
    // 충돌처리 대상
    [SerializeField] private LayerMask collisionLayer;
    // 저장해둘 것
    private RangeWeaponHandler rangeWeaponHandler;
    private bool isReady;
    private float currentDuration; // 시간 초과
    private Vector2 direction;

    // 초기화 필요한 정보
    private Transform pivot;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0); // 바로 하위에 있는거 쓸꺼라
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    // 초기화에 필요한 값 정부 생성
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        // 저장
        this.rangeWeaponHandler = weaponHandler;
        this.direction = direction;

        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        // Vector3.right랑 다름
        transform.right = this.direction;

        if (this.direction.x < 0)
            // 피벗 회전 시켜줘야 투사체가 제대로 보임
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            // 상하 반전
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    // 파괴
    private void DestroyProjectile(Vector3 position)
    {
        Destroy(this.gameObject);
    }

    // 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 설정한 레이어에 충돌된 것 인지 확인
        if (collisionLayer.value == (collisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            // 설정한 레이어가 아닌 Object 충돌시 데미지, 넉백 처리
            // 데미지 처리를 위해 ResourceController 호출
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                // 데미지 적용
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if (rangeWeaponHandler.IsOnKnockback) // 넉백 켜져있다면 넉백 처리
                {
                    // 넉백 처리를 위해 베이스 컨트롤러 호출해서 넉백 적용
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position));
        }
    }

}
