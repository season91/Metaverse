using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Flappy - Canvas�� ����
public class FlappyUIManager : MonoBehaviour
{
    public Image GameOverImage;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // �ٷδ� �ʿ� ���� ������ �Ⱥ��̰� Object�� ���ٴ� ��
        GameOverImage.gameObject.SetActive(false);
    }

    // ���� �غ� UI
    public void SetRestart()
    {
        GameOverImage.gameObject.SetActive(true);
    }


    // ���� ���� UI
    public void UpdateScore(int score)
    {
        Debug.Log(score);
        scoreText.text = score.ToString();
    }
}
