using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ó��
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Player �̵�, �ٶ󺸴� ó���� ���� ȣ��
    public PlayerController Player {  get; private set; }

    // UI ��ȯ�� ���� ȣ��
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);

        uiManager = FindObjectOfType<UIManager>();
    }

    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }

}
