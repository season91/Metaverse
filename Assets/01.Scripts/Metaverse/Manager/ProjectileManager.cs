using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // ������ ���� �̱��� ó��
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    private void Awake()
    {
        instance = this;
    }

    // ����ü ����
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);
        // ProjectileController ���� �ش� �߻�ü ������ ������ ������ �߻� ���� ����
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this); // �ʱ�ȭ �۾� ProjectileController�� �ʱ� ���� ����
    }

}
