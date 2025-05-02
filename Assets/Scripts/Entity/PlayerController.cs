using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조
    private Camera _camera; 
    private GameManager gameManager;

    // [화면에 실제로 움직여지게 할 요소]
    // 1. 캐릭터 실제 이동 처리를 위한 호출
    private Rigidbody2D _rigidbody;
    // 2. 캐릭터
    [SerializeField] private SpriteRenderer characterRenderer;
    // 3. 캐릭터 무기
    [SerializeField] private Transform weaponPivot;


    // [캐릭터 움직임에 필요한 변수]
    // 1. 이동하는 방향 지정 Move
    private Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    // 2. 바라보는 방향 지정 Look
    private Vector2 lookDirection = Vector2.zero;
    private Vector2 LookDirection { get { return lookDirection; } }
    
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

    // GameManager 에서 호출해서 초기화해줄 것임
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
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

    // InputSystem 적용
    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    private void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}
