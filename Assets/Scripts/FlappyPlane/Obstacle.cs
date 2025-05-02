using UnityEngine;

// Flappy - Obstacle Prefab에 연결
public class Obstacle : MonoBehaviour
{
    // 장애물 생성을 위한 변수들
    // 장애물이 상하로 얼마나 이동할 것인지 범위 지정 용도의 변수
    public float highPositionY = 1f;
    public float lowPositionY = -1f;

    // 홀사이즈는 Top, Bottom 사이의 공간을 얼마로 가져갈 것인지에 대한 변수
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    // Object들 패치 할때 사이 폭을 얼마나 가져갈 건지
    public float widthPadding = 4f;

    // 장애물 Top Bottom Object
    public Transform topObject;
    public Transform bottomObject;

    // start 에서 초기화
    private FlappyGameManager gameManager; // 게임 로직 진행을 위해 호출
    private void Start()
    {
        gameManager = FlappyGameManager.Instance;
    }

    // 장애물 랜덤 생성 로직
    public Vector3 SetRandomPlace(Vector3 lastposition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        // holeSize만큼 두 Object를 벌림
        // localPosition 은 부모 기준
        topObject.localPosition = new Vector3(0, halfHoleSize); // 반만큼 위로 올리고
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // 반만큼 아래로 내리고

        // 마지막 object 뒤에다가 공간만큼 더한 값으로 이동시켜 배치
        Vector3 placePosition = lastposition + new Vector3(widthPadding, 0);

        placePosition.y = Random.Range(lowPositionY, highPositionY);
        transform.position = placePosition;

        return placePosition;
    }

    // 장애물 통과시 점수 부여
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            gameManager.AddScore(1);
        }
    }
}
