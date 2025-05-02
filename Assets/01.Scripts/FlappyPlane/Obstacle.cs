using UnityEngine;

// Flappy - Obstacle Prefab�� ����
public class Obstacle : MonoBehaviour
{
    // ��ֹ� ������ ���� ������
    // ��ֹ��� ���Ϸ� �󸶳� �̵��� ������ ���� ���� �뵵�� ����
    public float highPositionY = 1f;
    public float lowPositionY = -1f;

    // Ȧ������� Top, Bottom ������ ������ �󸶷� ������ �������� ���� ����
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    // Object�� ��ġ �Ҷ� ���� ���� �󸶳� ������ ����
    public float widthPadding = 4f;

    // ��ֹ� Top Bottom Object
    public Transform topObject;
    public Transform bottomObject;

    // start ���� �ʱ�ȭ
    private FlappyGameManager gameManager; // ���� ���� ������ ���� ȣ��
    private void Start()
    {
        gameManager = FlappyGameManager.Instance;
    }

    // ��ֹ� ���� ���� ����
    public Vector3 SetRandomPlace(Vector3 lastposition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        // holeSize��ŭ �� Object�� ����
        // localPosition �� �θ� ����
        topObject.localPosition = new Vector3(0, halfHoleSize); // �ݸ�ŭ ���� �ø���
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // �ݸ�ŭ �Ʒ��� ������

        // ������ object �ڿ��ٰ� ������ŭ ���� ������ �̵����� ��ġ
        Vector3 placePosition = lastposition + new Vector3(widthPadding, 0);

        placePosition.y = Random.Range(lowPositionY, highPositionY);
        transform.position = placePosition;

        return placePosition;
    }

    // ��ֹ� ����� ���� �ο�
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            gameManager.AddScore(1);
        }
    }
}
