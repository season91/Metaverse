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


    // 3. ����-Ȱ-���Ÿ� ������ ���� ȣ��
    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;
    protected bool isAttacking; // InputSystem���� value set
    
    private float timeSinceLastAttack = float.MaxValue;
    // �˹鿡 ���� ����
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;


    // 4. ���� ����� ���� ȣ��
    protected StatHandler statHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<StatHandler>();
        // ���� ���� ��� ���� ȣ��
        // weaponPivot�� ���� WeaponPrefab ���� ����
    }

    private void Update()
    {
        // lookDirection ���� InputSystem OnLook���� ����
        Rotate(lookDirection);
        HandleAction();

        if (GameManager.Instance.isWaveGamePlaying && weaponHandler == null)
        {
            if (WeaponPrefab != null)
                weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
            else
                // �̹� ���� �����ϰ� �ִٸ� ��������
                weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
        // ���� ȣ��
        HandleAttackDelay();  // ���� �ð����� �߻��� �� �ְ� ���ְڴٴ� �ǹ�
    }

    private void FixedUpdate()
    {
        // movementDirection���� InputSystem OnMove���� ����
        Movment(movementDirection);
    }

    protected virtual void HandleAction()
    {
        // enemy���� �����
    }

    // �̵��� ���� ó��->���� �������� FixedUpdate���� ȣ��
    private void Movment(Vector2 direction)
    {
        // statHandler�� �ӵ�
        direction = direction * statHandler.Speed;
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

        // ���� ȸ��
        weaponHandler?.Rotate(isLeft);
    }

    // ����
    // ���� ������ ����
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

    // Attack�� �� �����̷� ȣ���Ұ���
    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

    // �˹��� �󸶳�, ��ŭ ������ ������
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
}
