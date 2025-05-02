using UnityEngine;


// Flappy - GameManager 연결
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
        // 게임 끝날 땐 ui restart 출력 호출
        Debug.Log("Game Over");
    }

}
