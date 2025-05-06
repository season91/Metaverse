using UnityEngine;

// Metaverse - Arrow ������ ����
// ���Ÿ� ���� �߻�ü ���� - �ʱ�ȭ, �浹ó��, �ı�
public class ProjectileController : MonoBehaviour
{
    // �浹ó�� ���
    [SerializeField] private LayerMask collisionLayer;
    // �����ص� ��
    private RangeWeaponHandler rangeWeaponHandler;
    private bool isReady;
    private float currentDuration; // �ð� �ʰ�
    private Vector2 direction;

    // �ʱ�ȭ �ʿ��� ����
    private Transform pivot;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0); // �ٷ� ������ �ִ°� ������
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    // �ʱ�ȭ�� �ʿ��� �� ���� ����
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        // ����
        this.rangeWeaponHandler = weaponHandler;
        this.direction = direction;

        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        // Vector3.right�� �ٸ�
        transform.right = this.direction;

        if (this.direction.x < 0)
            // �ǹ� ȸ�� ������� ����ü�� ����� ����
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            // ���� ����
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    // �ı�
    private void DestroyProjectile(Vector3 position)
    {
        Destroy(this.gameObject);
    }

    // �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ ���̾ �浹�� �� ���� Ȯ��
        if (collisionLayer.value == (collisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            // ������ ���̾ �ƴ� Object �浹�� ������, �˹� ó��
            // ������ ó���� ���� ResourceController ȣ��
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                // ������ ����
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if (rangeWeaponHandler.IsOnKnockback) // �˹� �����ִٸ� �˹� ó��
                {
                    // �˹� ó���� ���� ���̽� ��Ʈ�ѷ� ȣ���ؼ� �˹� ����
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position));
        }
    }

}
