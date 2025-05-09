using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WeaponHandler : MonoBehaviour
{
    // 무기, 공격에 필요한 정보
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    // 무기 사이즈
    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    // 공격력
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    // 공격속도
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }
    // 공격 범위 
    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    // 레이어 타겟
    public LayerMask target;

    // 넉백 정보 
    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    // 무기 공격시 소리 적용을 위해 호출
    public AudioClip attackSoundClip;

    // 초기화 필요한 정보들
    public BaseController Controller { get; private set; }
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    // 트리거로 만든 변수 불러옴
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    protected virtual void Awake()
    {
        // 이번엔 반대로 위로 올라가서 찾아와야함, 무기가 캐릭터 하위에 생겨야 하기 때문
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }
    protected virtual void Start()
    {
        // 상속 받아 구현
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
