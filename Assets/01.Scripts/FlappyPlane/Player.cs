using UnityEngine;

// Flappy - Player ����
public class Player : MonoBehaviour
{
    // Start���� �ʱ�ȭ �� ������
    private Rigidbody2D _rigidbody; // ���� �̵� ó���� ���� ȣ��
    private Animator animator; // �ִϸ��̼� ��ȯ�� ���� ȣ��
    private FlappyGameManager gameManager; // ���� ���� ������ ���� ȣ��

    // �̵� ó���� ���� ����ϴ� ����
    public float forwardSpeed = 3f; // ������ ���� �̵� �ϴ� ��
    public float flapForce = 6f; // �����ϴ� ��

    // ���� ������ ���� ����ϴ� ����
    private float deathCooldown = 0f; // �浹�� ���� �ð� �� �׵���
    private bool isFlap = false; // �̵��ϴ��� ����
    public bool isDead = false; // �׾����� ����

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        gameManager = FlappyGameManager.Instance;
    }

    // ���콺 Ŭ���� �Ǵ� �����̽��� ���� ��� �̵� ó��
    void Update()
    {
        // �׾����� ���� restart ȣ��
        if(isDead)
        {
            if (deathCooldown <= 0)
            {
                // ���� ����. ��Ÿ������ ����
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.EndGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; // ���� ó��
                FixedUpdate(); // ���� ������ �ʿ��ϹǷ� FixedUpdate���� �̵� ó��
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // ����� ����
        Movement();
        // ����� ȸ��
        Rotate(); ;
    }

    private void Movement()
    {
        // �Ȱ��� �ӵ��� ����
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        // ����� ���� ó��
        if (isFlap)
        {
            velocity.y += flapForce; // �����ϴ� �� �����ֱ�
            isFlap = false; // ��������� false ó��
        }

        _rigidbody.velocity = velocity; // ���� ����
    }

    private void Rotate()
    {
        // ����� ����
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // �����̵� �浹�� ���� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        // ������ 1�� �ִ� ����� ó��
        isDead = true;
        deathCooldown = 1f;

        // Die �ִϸ��̼� ����, ���� ����
        animator.SetBool("IsDie", true);
        gameManager.GameOver();
    }

}
