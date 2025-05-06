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

    // 3���� �ִϸ��̼� ��ȯ ������ BaseController���� ȣ��
    // Movement���� ȣ��
    public void Move(Vector2 obj)
    {
        // ������ ũ�⸦ ��
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    // ������ ������ �ð�
    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }

}
