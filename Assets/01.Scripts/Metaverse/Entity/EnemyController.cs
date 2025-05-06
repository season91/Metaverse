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

            // ���� �ݰ� ����� ���� ��ƾ
            if (distance <= weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                       (1 << LayerMask.NameToLayer("Map")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                // ���� ����� ���� �̵��ϱ�
                movementDirection = direction;
                return;
            }

            // ���ݹ��� �ƴ϶�� �̵���. �Ѿư��� �Ÿ�
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
        enemyManager.RemoveEnemyOnDeath(this);
    }

}
