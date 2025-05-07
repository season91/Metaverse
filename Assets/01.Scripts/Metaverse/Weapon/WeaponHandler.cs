using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WeaponHandler : MonoBehaviour
{
    // ����, ���ݿ� �ʿ��� ����
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    // ���� ������
    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    // ���ݷ�
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    // ���ݼӵ�
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }
    // ���� ���� 
    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    // ���̾� Ÿ��
    public LayerMask target;

    // �˹� ���� 
    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    // ���� ���ݽ� �Ҹ� ������ ���� ȣ��
    public AudioClip attackSoundClip;

    // �ʱ�ȭ �ʿ��� ������
    public BaseController Controller { get; private set; }
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    // Ʈ���ŷ� ���� ���� �ҷ���
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    protected virtual void Awake()
    {
        // �̹��� �ݴ�� ���� �ö󰡼� ã�ƿ;���, ���Ⱑ ĳ���� ������ ���ܾ� �ϱ� ����
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }
    protected virtual void Start()
    {
        // ��� �޾� ����
    }

    public virtual void Attack()
    {
        AttackAnimation();

        if (attackSoundClip != null)
            SoundManager.Instance.PlayClip(attackSoundClip);
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
