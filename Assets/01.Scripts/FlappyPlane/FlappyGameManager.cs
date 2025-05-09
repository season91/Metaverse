using UnityEngine;
using UnityEngine.SceneManagement;


// Flappy - GameManager 연결
public class FlappyGameManager : MonoBehaviour
{
    // 싱글톤
    private static FlappyGameManager flappyGameManager;
    // ui매니저 접근위해 호출, awake 에서 초기화
    private FlappyUIManager uiManager;
    public static FlappyGameManager Instance { get { return flappyGameManager; } }

    // 점수 변수
    private int currentScore = 0;

    private void Awake()
    {
        flappyGameManager = this;
        uiManager = FindObjectOfType<FlappyUIManager>(); // gameManager 같은 선상에 Canvas-ui있음
    }

    private void Start()
    {
        // 처음 start됐을 때 ui 0점 처리로 시작
        uiManager.UpdateScore(0);
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("FlappyGameScore", currentScore);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Metaverse");
    }

    public void GameOver()
    {
        uiManager.SetGameOver();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
    }

    public void StartUI(bool enable)
    {
        uiManager.SetGameStart(enable);
    }
}
