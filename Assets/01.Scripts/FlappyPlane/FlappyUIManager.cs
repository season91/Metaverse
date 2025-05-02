using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas�� ����
public class FlappyUIManager : MonoBehaviour
{
    public Image GameOverImage;
    public TextMeshProUGUI GameStartText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // �ٷδ� �ʿ� ���� ������ �Ⱥ��̰� Object�� ���ٴ� ��
        GameStartText.gameObject.SetActive(false);
    }

    // ���� �غ� UI
    public void SetRestart()
    {
        GameStartText.gameObject.SetActive(true);
    }

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
