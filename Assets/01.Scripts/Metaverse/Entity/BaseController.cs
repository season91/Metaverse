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

    // 5. �ִϸ��̼ǻ���� ���� ȣ��
    protected AnimationHandler animationHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();

        // weaponPivot�� ���� WeaponPrefab ���� ����
        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            // �̹� ���� �����ϰ� �ִٸ� ��������
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    private void Update()
    {
        // lookDirection ���� InputSystem OnLook���� ����
        Rotate(lookDirection);
        HandleAction();
        HandleAttackDelay();  // ���� �ð����� �߻��� �� �ְ� ���ְڴٴ� �ǹ�
    }

    private void FixedUpdate()
    {
        // movementDirection���� InputSystem OnMove���� ����
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
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

        // �˹�ó��
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
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

    // ��� ó��
    public virtual void Death()
    {
        // ������ ����
        _rigidbody.velocity = Vector3.zero;

        // ���� ��� ��������Ʈ ã�ƿͼ� ����
        // ��� SpriteRenderer�� ���� ���缭 ���� ȿ�� ����
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        //  ��� ������Ʈ(��ũ��Ʈ ����) ��Ȱ��ȭ
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // 2���ִ� ������Ʈ �ı�
        Destroy(gameObject, 2f);
    }
}
