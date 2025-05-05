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

    // �� ������ ���� ȣ��
    private EnemyManager enemyManager;
    public static bool isFirstLoading = true; // ù �ε����� ����

    // �� ���̺� ���� ���� ����
    [SerializeField] private int currentWaveIndex = 0;

    // �̴ϰ��� ���� ����
    // ����
    private int score = 0;
    public int Score { get => score; }
    private const string MiniGameScoreKey = "FlappyGameScore";
    // �ְ� ����
    private int bestScore = 0;
    public int BestScore { get => bestScore; }
    private const string MiniGameBestScoreKey = "FlappyGameBestScore";

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);

        // UIManager �ʱ�ȭ - ��ȯ�� ����
        uiManager = FindObjectOfType<UIManager>();

        // EnemyManager �ʱ�ȭ - �� ������ ����
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }

    // ��Ÿ���� ������ ���� �ε�
    private void Start()
    {
        score = PlayerPrefs.GetInt(MiniGameScoreKey, 0);
        bestScore = PlayerPrefs.GetInt(MiniGameBestScoreKey, 0);
        SetMiniGameScoreUI();

        if (!isFirstLoading)
        {
            StartGame(); // �� ��° ���� �� �ε� �� �ڵ� ����
        }
        else
        {
            isFirstLoading = false; // ù �ε� ����
        }
    }

    // �̴ϰ��� ���� ���Խ� Popup On
    public void EnterMiniGameZone()
    {
        uiManager.ChangeState(UIState.Popup);
    }

    // �̴ϰ��� ���� ��Ż�� Score On
    public void ExitMiniGameZone()
    {
        uiManager.ChangeState(UIState.Score);
        SetMiniGameScoreUI();
    }

    // �̴ϰ��� ���� - Popup start button ���ý� ȣ��
    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }

    // �̴ϰ��� ���� �� Score UI �ݿ�
    public void SetMiniGameScoreUI()
    {
        UpdateMiniGameScore();
        uiManager.SetScoreUI();
    }

    // �̴ϰ��� ���� ���� Ȯ��
    private void UpdateMiniGameScore()
    {
        if (bestScore < score)
        {
            Debug.Log("�ְ� ���� ����");
            bestScore = score;
            PlayerPrefs.SetInt(MiniGameBestScoreKey, bestScore);
            PlayerPrefs.Save();
        }
    }

    // �� ���� ����
    public void StartGame()
    {
        StartNextWave();
    }
    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // �������� ���� ���̺� ����
    public void EndOfWave()
    {
        StartNextWave();
    }

    // ����
    public void GameOver()
    {
        enemyManager.StopWave();
    }
}

