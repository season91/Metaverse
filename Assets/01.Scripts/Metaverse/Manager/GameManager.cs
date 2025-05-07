using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ó��
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // ü�º���
    private ResourceController _playerResourceController;

    // Player �̵�, �ٶ󺸴� ó���� ���� ȣ��
    public PlayerController Player {  get; private set; }

    // UI ��ȯ�� ���� ȣ��
    private UIManager uiManager;

    // Wave ���� ���� ����
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;
    private const string WaveGameBestScoreKey = "WaveGameBestKill";
    public int Kill { get; private set; }
    public int BestKill { get; private set; }

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

        // UIManager �ʱ�ȭ - ��ȯ�� ����
        uiManager = UIManager.Instance;

        // �� ���� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    
    public void WaveGameOver()
    {
        currentWaveIndex = 0;
        enemyManager.StopWave();
        uiManager.SetGameUI(false);
        UpdateKill();
        SceneManager.LoadScene("Metaverse");
    }

    public void SetMiniGamePopup(Vector3 miniGamePosition, bool isEnter)
    {
        uiManager.SetPressUI(miniGamePosition, isEnter);
    }

    // Kill �� ���� 
    public void AddKill(int killCount)
    {
        Kill += killCount;
    }

    private void UpdateKill()
    {
        if (BestKill < Kill)
        {
            BestKill = Kill;

            // PlayerPrefs�� ����
            PlayerPrefs.SetInt(WaveGameBestScoreKey, BestKill);
            PlayerPrefs.Save();
        }
    }

    // �� �̵��� �� ������ �ʿ��� �� �ʱ�ȭ (GameManager DontDestroyOnLoad��)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Dunjeon �������� �ʱ�ȭ
        if (scene.name == "Dunjeon")
        {
            enemyManager = FindObjectOfType<EnemyManager>();
            if (enemyManager != null)
            {
                Player = FindObjectOfType<PlayerController>();
                Player.Init(this);

                _playerResourceController = Player.GetComponent<ResourceController>();

                // ü�� ���� �̺�Ʈ�� UI�� ���� : ���ҽ���Ʈ�ѷ��� �˾Ƽ� �� �ϴٰ� ü�� ���� �̺�Ʈ �߻���
                // �ߺ� ��� ������ ���� ���� ������ �� 
                _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
                // �ٽ� ���
                _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

                enemyManager.Init(this, uiManager);
                uiManager.SetGameUI(true);
                StartNextWave();
            }
        }

        // Metaverse �������� �ʱ�ȭ
        if (scene.name == "Metaverse")
        {
            Score = PlayerPrefs.GetInt(MiniGameScoreKey, 0);
            BestScore = PlayerPrefs.GetInt(MiniGameBestScoreKey, 0);
            BestKill = PlayerPrefs.GetInt(WaveGameBestScoreKey, 0);
            uiManager.SetScoreUI(true);
        }

        if (scene.name == "FlappyPlane")
        {
            uiManager.SetScoreUI(false);
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

