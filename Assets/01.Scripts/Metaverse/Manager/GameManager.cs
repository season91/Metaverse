using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ó��
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // ü�º���
    private ResourceController _playerResourceController;

    // ���� ������ ����
    private EnemyController _enemyController;

    // Player �̵�, �ٶ󺸴� ó���� ���� ȣ��
    public PlayerController Player {  get; private set; }

    // UI ��ȯ�� ���� ȣ��
    private UIManager uiManager;

    // Wave ���� ���� ����
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;

    // �̴ϰ��� ���� ����
    private const string MiniGameScoreKey = "FlappyGameScore";
    private const string MiniGameBestScoreKey = "FlappyGameBestScore";
    public int Score { get; private set; }
    public int BestScore { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �̹� ����ִ� �� ������, ���� ���� �� ����
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);

        // UIManager �ʱ�ȭ - ��ȯ�� ����
        uiManager = FindObjectOfType<UIManager>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this, uiManager);

        _playerResourceController = Player.GetComponent<ResourceController>();

        // ü�� ���� �̺�Ʈ�� UI�� ���� : ���ҽ���Ʈ�ѷ��� �˾Ƽ� �� �ϴٰ� ü�� ���� �̺�Ʈ �߻���
        // �ߺ� ��� ������ ���� ���� ������ �� 
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        // �ٽ� ���
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

    }

    private void Start()
    {
        Score = PlayerPrefs.GetInt(MiniGameScoreKey, 0);
        BestScore = PlayerPrefs.GetInt(MiniGameBestScoreKey, 0);
    }

    // �̴ϰ��� ����
    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }
    // �� ���� ����
    public void StartWaveGame()
    {
        SceneManager.LoadScene("Dunjeon");
        uiManager.SetScoreUI(false, Score, BestScore);
        uiManager.SetGameUI(true);
        StartNextWave();
    }
    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }
    public void EndOfWave()
    {
        StartNextWave();
    }
    
    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameUI(false);
        uiManager.SetScoreUI(true, Score, BestScore);
        
        SceneManager.LoadScene("Metaverse");
    }

    public void SetMiniGamePopup(Vector3 miniGamePosition, bool isEnter)
    {
        uiManager.SetPressUI(miniGamePosition, isEnter);
    }
}

