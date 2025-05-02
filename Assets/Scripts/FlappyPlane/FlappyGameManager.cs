using UnityEngine;


// Flappy - GameManager ����
public class FlappyGameManager : MonoBehaviour
{
    private static FlappyGameManager flappyGameManager;

    public static FlappyGameManager Instance { get { return flappyGameManager; } }

    private void Awake()
    {
        flappyGameManager = this;
    }

    public void GameOver()
    {
        // ���� ���� �� ui restart ��� ȣ��
        Debug.Log("Game Over");
    }

}
