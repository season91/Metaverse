using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 처리
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // 체력변경
    private ResourceController _playerResourceController;

    // Player 이동, 바라보는 처리를 위한 호출
    public PlayerController Player {  get; private set; }

    // UI 전환을 위해 호출
    private UIManager uiManager;

    // Wave 게임 관련 변수
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;
    private const string WaveGameBestScoreKey = "WaveGameBestKill";
    public int Kill { get; private set; }
    public int BestKill { get; private set; }

    // 미니게임 관련 변수
    private const string MiniGameScoreKey = "FlappyGameScore";
    private const string MiniGameBestScoreKey = "FlappyGameBestScore";
    public int Score { get; private set; }
    public int BestScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 이미 살아있는 게 있으면, 새로 생긴 건 삭제
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // UIManager 초기화 - 전환을 위해
        uiManager = UIManager.Instance;

        // 씬 변경 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 미니게임 시작
    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }
    // 맵 게임 시작
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

    // Kill 수 증가 
    public void AddKill(int killCount)
    {
        Kill += killCount;
    }

    private void UpdateKill()
    {
        if (BestKill < Kill)
        {
            BestKill = Kill;

            // PlayerPrefs에 저장
            PlayerPrefs.SetInt(WaveGameBestScoreKey, BestKill);
            PlayerPrefs.Save();
        }
    }

    // 씬 이동시 각 씬에서 필요한 것 초기화 (GameManager DontDestroyOnLoad라서)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Dunjeon 씬에서만 초기화
        if (scene.name == "Dunjeon")
        {
            enemyManager = FindObjectOfType<EnemyManager>();
            if (enemyManager != null)
            {
                Player = FindObjectOfType<PlayerController>();
                Player.Init(this);

                _playerResourceController = Player.GetComponent<ResourceController>();

                // 체력 변경 이벤트를 UI에 연결 : 리소스컨트롤러가 알아서 일 하다가 체력 변경 이벤트 발생시
                // 중복 등록 방지를 위해 먼저 제거한 뒤 
                _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
                // 다시 등록
                _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

                enemyManager.Init(this, uiManager);
                uiManager.SetGameUI(true);
                StartNextWave();
            }
        }

        // Metaverse 씬에서만 초기화
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

