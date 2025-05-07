using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas에 연결
public class FlappyUIManager : MonoBehaviour
{
    public Image gameOverImage;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gamestartText;

    // 게임 Over UI
    public void SetGameOver()
    {
        gameOverImage.gameObject.SetActive(true);
    }

    // 게임 점수 UI
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // 게임 시작 UI
    public void SetGameStart(bool isEnable)
    {
        gamestartText.gameObject.SetActive(isEnable);
    }
}
