using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas�� ����
public class FlappyUIManager : MonoBehaviour
{
    public Image GameOverImage;
    public TextMeshProUGUI scoreText;

    // ���� Over UI
    public void SetGameOver()
    {
        GameOverImage.gameObject.SetActive(true);
    }


    // ���� ���� UI
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
