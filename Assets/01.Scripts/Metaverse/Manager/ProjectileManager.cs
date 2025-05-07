using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // ������ ���� �̱��� ó��
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    [SerializeField] private ParticleSystem impactParticlePrefab;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �ߺ� ���� ����
            return;
        }
        instance = this;
    }

    // ����ü ����
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this); // �ʱ�ȭ �۾� ProjectileController�� �ʱ� ���� ����
    }
    public void CreateImpactParticlesAtPostion(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        // ��ƼŬ ������ ����
        ParticleSystem psObj = Instantiate(impactParticlePrefab, position, Quaternion.identity);

        ParticleSystem.EmissionModule em = psObj.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        ParticleSystem.MainModule mainModule = psObj.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;

        // ��ƼŬ ���
        psObj.Play();

        // ��ƼŬ�� ���� �� ����
        float fasterDuration = psObj.main.duration * 0.5f;
        Destroy(psObj.gameObject, fasterDuration); 
    }
}
