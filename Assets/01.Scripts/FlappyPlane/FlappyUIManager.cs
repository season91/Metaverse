using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas에 연결
public class FlappyUIManager : MonoBehaviour
{
    public Image GameOverImage;
    public TextMeshProUGUI GameStartText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // 바로는 필요 없기 때문에 안보이게 Object를 끈다는 것
        GameStartText.gameObject.SetActive(false);
    }

    // 게임 준비 UI
    public void SetRestart()
    {
        GameStartText.gameObject.SetActive(true);
    }

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
