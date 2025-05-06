using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    // �߻� ���� ����
    [Header("Ranged Attack Data")]
    // �߻�ü ���� ��ġ
    [SerializeField] private Transform projectileSpawnPosition;
    // �߻�ü ������
    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } }
    // �߻�ü index ��ȣ(Arrow���� Skull����)
    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }
    // �߻�ü ���󰡴� ���� �󸶳� ������ �������� ���� ����
    [SerializeField] private float duration;
    public float Duration { get { return duration; } }
    // �߻�ü � ��� �Ұ��� ����
    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }
    // �߻�ü ���� ���� ����
    [SerializeField] private float spread;
    public float Spread { get { return spread; } }
    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }
    // �߻�ü ����
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    // �߻�ü ������ ���� ȣ��
    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }


    // ����
    public override void Attack()
    {
        base.Attack();
        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;


        // �ּ�ġ �ޱ�, ���⼭ ���� �� �߻��Ұ��̱⶧���� ���� ���ؾ���
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            // �߻� ���� ��ŭ ���� �̵��ؼ� ���
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread); // ������ ź ���� ����
            angle += randomSpread; // ��ä�Ӱ� ������ �Ѿ��� �� ��

            CreateProjectile(Controller.LookDirection, angle); // �߻�ü ����
        }
    }

    // �Ŵ����� ���ؼ� �߻�ü ����
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
               this,
               projectileSpawnPosition.position,
               RotateVector2(_lookDirection, angle));
    }
    // ���Ϸ��� ������ ����� ���͸� ���ؼ� ����
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // Quaternion�� ȸ���� ��ġ��ŭ ���͸� ȸ�������� �� ���� ��ü�� ȸ���� �����ֱ� ���� ����ϴ� �� 
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
