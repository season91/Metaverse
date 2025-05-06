using UnityEngine;

// Metavers - Goblin ����
public class EnemyController : BaseController
{
    // ���� ���
    private Transform target;
    [SerializeField] private float followRange = 15f; // Ÿ���� �Ѿư� �ִ� �Ÿ�

    // �Ŵ��� ȣ��
    private EnemyManager enemyManager;

    // �� ������ �ʱ�ȭ
    public void Init(Transform target, EnemyManager enemyManager)
    {
        this.target = target;
        this.enemyManager = enemyManager;
    }

    // BaseController Update���� ȣ��
    // ���� �̵�, ȸ�� ó��
    protected override void HandleAction()
    {
        base.HandleAction();

        // tartget ����ó��
        // ���� ������ ���� ������ �ϱ� ������ EnemyController �ʱ�ȭ ���� ���� ���·� ���� �߻��� �� ����
        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false; // ������ fasle 
        if (distance <= followRange) // ���� �Ÿ� �ȿ� �������� ���� ��ȯ ó��
        {
            lookDirection = direction; // �ٶ� �� �ְ� look ����

            // ���� ����� ���� �̵��ϱ�
            movementDirection = direction;
        }
    }

    // ���� ������ �Ÿ�
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    // ���� ����� ���� ���� ����
    protected Vector2 DirectionToTarget()
    {
        // ������ ����
        return (target.position - transform.position).normalized;
    }
    public override void Death()
    {
        base.Death();
        // �Ŵ����� ���� ȣ��
        enemyManager.RemoveEnemyOnDeath(this);
    }

}
