using UnityEngine;

// Flappy - Main Camera - BackGroundLopper 연결(충돌 컴포넌트만 있음)
public class BackGroundLopper : MonoBehaviour
{
    private int numBgCount = 5; // 배경 개수
    private int obstacleCount = 0; // 장애물 개수
    private Vector3 obstacleLastPosition = Vector3.zero; // 장애물 마지막 위치. 0,0,0 위치로 시작

    private void Start()
    {
        // 장애물 전부 찾아와 랜덤 배치 해줄 것
        // 여러 object들을 가져 올 것이기 때문에 가볍진 않기 때문에 start, awake 1회 동작만 하게 하는 것이 좋음
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            // 장애물 개수 만큼 랜덤 배치
            // 배치한 위치 받아 와서 그 다음 장애물이 배치될 곳을 전달해줌
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // 충돌 대상 기준으로 이어 붙여주는 로직
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround")) // 배경을 뒤로 이어 붙여줌
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position; //충돌한애 위치 

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;

            return;
        }

        // 장애물이면 장애물 배치
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle) // null이 아니라면 장애물이므로 랜덤 배치 다시 해줄 것
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
