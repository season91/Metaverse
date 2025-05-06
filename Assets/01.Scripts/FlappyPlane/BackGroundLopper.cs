using UnityEngine;

// Flappy - Main Camera - BackGroundLopper ����(�浹 ������Ʈ�� ����)
public class BackGroundLopper : MonoBehaviour
{
    private int numBgCount = 5; // ��� ����
    private int obstacleCount = 0; // ��ֹ� ����
    private Vector3 obstacleLastPosition = Vector3.zero; // ��ֹ� ������ ��ġ. 0,0,0 ��ġ�� ����

    private void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // �浹 ��� �������� �̾� �ٿ��ִ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ֹ��̸� ��ֹ� ��ġ
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle) // null�� �ƴ϶�� ��ֹ��̹Ƿ� ���� ��ġ �ٽ� ���� ��
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
