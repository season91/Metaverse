using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�ϱ� ���� ���� ī�޶� ����
    private Camera _camera; 
    private GameManager gameManager;

    // [ȭ�鿡 ������ ���������� �� ���]
    // 1. ĳ���� ���� �̵� ó���� ���� ȣ��
    private Rigidbody2D _rigidbody;
    // 2. ĳ����
    [SerializeField] private SpriteRenderer characterRenderer;
    // 3. ĳ���� ����
    [SerializeField] private Transform weaponPivot;


    // [ĳ���� �����ӿ� �ʿ��� ����]
    // 1. �̵��ϴ� ���� ���� Move
    private Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    // 2. �ٶ󺸴� ���� ���� Look
    private Vector2 lookDirection = Vector2.zero;
    private Vector2 LookDirection { get { return lookDirection; } }
    
    private void Update()
    {
        // lookDirection ���� InputSystem OnLook���� ����
        Rotate(lookDirection); 
    }

    private void FixedUpdate()
    {
        // movementDirection���� InputSystem OnMove���� ����
        Movment(movementDirection);
    }

    // GameManager ���� ȣ���ؼ� �ʱ�ȭ���� ����
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    // �̵��� ���� ó��->���� �������� FixedUpdate���� ȣ��
    private void Movment(Vector2 direction)
    {
        // �̵��ӵ� 5�� ���߿� �������� ������ ��
        direction = direction * 5;
        _rigidbody.velocity = direction;
    }

    // �ٶ󺸴� �������� ĳ���� ȸ�� ó��
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

    // InputSystem ����
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
