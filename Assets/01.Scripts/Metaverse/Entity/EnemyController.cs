using UnityEngine;

// Metavers - Goblin 연결
public class EnemyController : BaseController
{
    // 공격 대상
    private Transform target;
    [SerializeField] private float followRange = 15f; // 타겟을 쫓아갈 최대 거리

    // 매니저 호출
    private EnemyManager enemyManager;

    // 적 생성시 초기화
    public void Init(Transform target, EnemyManager enemyManager)
    {
        this.target = target;
        this.enemyManager = enemyManager;
    }

    // BaseController Update에서 호출
    // 적의 이동, 회전 처리
    protected override void HandleAction()
    {
        base.HandleAction();

        // tartget 예외처리
        // 적을 프리팹 복사 생성을 하기 때문에 EnemyController 초기화 되지 않은 상태로 에러 발생할 수 있음
        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false; // 지금은 fasle 
        if (distance <= followRange) // 따라갈 거리 안에 들어왔으면 방향 전환 처리
        {
            lookDirection = direction; // 바라볼 수 있게 look 변경

            // 공격 반경 내라면 공격 루틴
            if (distance <= weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                       (1 << LayerMask.NameToLayer("Map")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                // 공격 대상을 향해 이동하기
                movementDirection = direction;
                return;
            }

            // 공격범위 아니라면 이동만. 쫓아가는 거리
            movementDirection = direction;
        }
    }

    // 공격 대상과의 거리
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    // 공격 대상을 향한 방향 벡터
    protected Vector2 DirectionToTarget()
    {
        // 방향을 구함
        return (target.position - transform.position).normalized;
    }
    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }

}
