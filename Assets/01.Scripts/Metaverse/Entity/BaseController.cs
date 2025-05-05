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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // lookDirection 값은 InputSystem OnLook에서 설정
        Rotate(lookDirection);
    }

    private void FixedUpdate()
    {
        // movementDirection값은 InputSystem OnMove에서 설정
        Movment(movementDirection);
    }

    // 이동에 대한 처리->물리 연산으로 FixedUpdate에서 호출
    private void Movment(Vector2 direction)
    {
        // 이동속도 5는 나중에 스텟으로 적용할 것
        direction = direction * 5;
        _rigidbody.velocity = direction;
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
    }
}
