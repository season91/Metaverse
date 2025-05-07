using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas�� ����
public class FlappyUIManager : MonoBehaviour
{
    public Image gameOverImage;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gamestartText;

    // ���� Over UI
    public void SetGameOver()
    {
        gameOverImage.gameObject.SetActive(true);
    }

    // ���� ���� UI
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // ���� ���� UI
    public void SetGameStart(bool isEnable)
    {
        gamestartText.gameObject.SetActive(isEnable);
    }
}
