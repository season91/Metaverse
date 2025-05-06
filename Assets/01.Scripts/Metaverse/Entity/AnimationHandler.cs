using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // 3가지 애니메이션 전환 시점을 BaseController에서 호출
    // Movement에서 호출
    public void Move(Vector2 obj)
    {
        // 벡터의 크기를 비교
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    // 무적이 끝나는 시간
    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }

}
