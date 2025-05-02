using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas에 연결
public class FlappyUIManager : MonoBehaviour
{
    public Image GameOverImage;
    public TextMeshProUGUI scoreText;

    // 게임 Over UI
    public void SetGameOver()
    {
        GameOverImage.gameObject.SetActive(true);
    }


    // 게임 점수 UI
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
