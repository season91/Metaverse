using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    // 발사 관련 변수
    [Header("Ranged Attack Data")]
    // 발사체 생성 위치
    [SerializeField] private Transform projectileSpawnPosition;
    // 발사체 사이즈
    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } }
    // 발사체 index 번호(Arrow인지 Skull인지)
    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }
    // 발사체 날라가는 동안 얼마나 생존해 있을지에 대한 변수
    [SerializeField] private float duration;
    public float Duration { get { return duration; } }
    // 발사체 몇개 쏘게 할건지 지정
    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }
    // 발사체 퍼짐 정보 지정
    [SerializeField] private float spread;
    public float Spread { get { return spread; } }
    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }
    // 발사체 색상
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    // 발사체 생성을 위한 호출
    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }


    // 공격
    public override void Attack()
    {
        base.Attack();
        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;


        // 최소치 앵글, 여기서 부터 쭉 발사할것이기때문에 각도 구해야함
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            // 발사 개수 만큼 각도 이동해서 쏜다
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread); // 랜덤의 탄 퍼짐 적용
            angle += randomSpread; // 다채롭게 나가는 총알이 될 것

            CreateProjectile(Controller.LookDirection, angle); // 발사체 생성
        }
    }

    // 매니저를 통해서 발사체 생성
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
               this,
               projectileSpawnPosition.position,
               RotateVector2(_lookDirection, angle));
    }
    // 오일러로 각도를 만들어 벡터를 곱해서 리턴
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // Quaternion의 회전의 수치만큼 벡터를 회전시켜줌 즉 벡터 자체에 회전을 가해주기 위해 사용하는 것 
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
