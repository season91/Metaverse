using UnityEngine;

// Metaverse - ������ ���ϰ�, Player�� Enemy���� �������� ����ϴ� ���� ����
public class BaseController : MonoBehaviour
{
    // [ȭ�鿡 ������ ���������� �� ���]
    // 1. ĳ���� ���� �̵� ó���� ���� ȣ��
    private Rigidbody2D _rigidbody;
    // 2. ĳ����
    [SerializeField] private SpriteRenderer characterRenderer;
    // 3. ĳ���� ����
    [SerializeField] private Transform weaponPivot;


    // [ĳ���� �����ӿ� �ʿ��� ����]
    // 1. �̵��ϴ� ���� ���� Move
    protected Vector2 movementDirection = Vector2.zero; // ��ӹ޴� ������ ����ؾ��ϹǷ� protected
    public Vector2 MovementDirection { get { return movementDirection; } }
    // 2. �ٶ󺸴� ���� ���� Look
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

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
}
