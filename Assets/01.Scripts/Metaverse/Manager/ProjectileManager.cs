using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // 접근을 위해 싱글톤 처리
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    [SerializeField] private ParticleSystem impactParticlePrefab;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
            return;
        }
        instance = this;
    }

    // 투사체 생성
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this); // 초기화 작업 ProjectileController에 초기 정보 전달
    }
    public void CreateImpactParticlesAtPostion(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        // 파티클 프리팹 생성
        ParticleSystem psObj = Instantiate(impactParticlePrefab, position, Quaternion.identity);

        ParticleSystem.EmissionModule em = psObj.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        ParticleSystem.MainModule mainModule = psObj.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;

        // 파티클 재생
        psObj.Play();

        // 파티클이 끝날 때 삭제
        float fasterDuration = psObj.main.duration * 0.5f;
        Destroy(psObj.gameObject, fasterDuration); 
    }
}
