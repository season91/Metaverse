using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // 접근을 위해 싱글톤 처리
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    private void Awake()
    {
        instance = this;
    }

    // 투사체 생성
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);
        // ProjectileController 에서 해당 발사체 프리팹 정보를 가지고 발사 해줄 것임
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this); // 초기화 작업 ProjectileController에 초기 정보 전달
    }

}
