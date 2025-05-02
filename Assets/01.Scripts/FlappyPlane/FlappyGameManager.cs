using UnityEngine;
using UnityEngine.SceneManagement;


// Flappy - GameManager ����
public class FlappyGameManager : MonoBehaviour
{
    // �̱���
    private static FlappyGameManager flappyGameManager;
    // ui�Ŵ��� �������� ȣ��, awake ���� �ʱ�ȭ
    private FlappyUIManager uiManager;
    public static FlappyGameManager Instance { get { return flappyGameManager; } }

    // ���� ����
    private int currentScore = 0;

    private void Awake()
    {
        flappyGameManager = this;
        uiManager = FindObjectOfType<FlappyUIManager>(); // gameManager ���� ���� Canvas-ui����
    }

    private void Start()
    {
        // ó�� start���� �� ui 0�� ó���� ����
        uiManager.UpdateScore(0);
    }

    public void EndGame()
    {
        Debug.Log("currentScore " + currentScore);
        PlayerPrefs.SetInt("FlappyGameScore", currentScore);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Metaverse");
    }

    public void GameOver()
    {
        // ���� ���� �� ui get ready �̹��� UI ȣ��
        //uiManager.SetRestart();
        uiManager.SetGameOver();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        // ui �� ���� ���
        uiManager.UpdateScore(currentScore);
    }

}
