using UnityEngine;

// Flappy - Player 연결
public class Player : MonoBehaviour
{
    // Start에서 초기화 할 변수들
    private Rigidbody2D _rigidbody; // 물리 이동 처리를 위해 호출
    private Animator animator; // 애니메이션 전환을 위해 호출
    private FlappyGameManager gameManager; // 게임 로직 진행을 위해 호출

    // 이동 처리를 위해 사용하는 변수
    public float forwardSpeed = 3f; // 앞으로 정면 이동 하는 힘
    public float flapForce = 6f; // 점프하는 힘

    // 게임 진행을 위해 사용하는 변수
    private float deathCooldown = 0f; // 충돌후 일정 시간 후 죽도록
    private bool isFlap = false; // 이동하는지 구분
    public bool isDead = false; // 죽었는지 구분

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        gameManager = FlappyGameManager.Instance;
    }

    // 마우스 클릭시 또는 스페이스바 누를 경우 이동 처리
    void Update()
    {
        // 죽었으면 게임 restart 호출
        if(isDead)
        {
            if (deathCooldown <= 0)
            {
                // 게임 종료. 메타버스로 복귀
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
                isFlap = true; // 점프 처리
                FixedUpdate(); // 물리 연산이 필요하므로 FixedUpdate에서 이동 처리
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // 비행기 전진
        Movement();
        // 비행기 회전
        Rotate(); ;
    }

    private void Movement()
    {
        // 똑같은 속도로 전진
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        // 비행기 점프 처리
        if (isFlap)
        {
            velocity.y += flapForce; // 점프하는 힘 더해주기
            isFlap = false; // 사용했으니 false 처리
        }

        _rigidbody.velocity = velocity; // 물리 적용
    }

    private void Rotate()
    {
        // 비행기 각도
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 무엇이든 충돌시 게임 종료
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        // 죽으면 1초 있다 재시작 처리
        isDead = true;
        deathCooldown = 1f;

        // Die 애니메이션 실행, 게임 종료
        animator.SetBool("IsDie", true);
        gameManager.GameOver();
    }

}
