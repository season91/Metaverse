using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 처리
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Player 이동, 바라보는 처리를 위한 호출
    public PlayerController Player {  get; private set; }
    
    // UI 전환을 위해 호출
    private UIManager uiManager;

    // 적 생성을 위해 호출
    private EnemyManager enemyManager;
    public static bool isFirstLoading = true; // 첫 로딩인지 구분

    // 적 웨이브 게임 관련 변수
    [SerializeField] private int currentWaveIndex = 0;

    // 미니게임 관련 변수
    // 점수
    private int score = 0;
    public int Score { get => score; }
    private const string MiniGameScoreKey = "FlappyGameScore";
    // 최고 점수
    private int bestScore = 0;
    public int BestScore { get => bestScore; }
    private const string MiniGameBestScoreKey = "FlappyGameBestScore";

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);

        // UIManager 초기화 - 전환을 위해
        uiManager = FindObjectOfType<UIManager>();

        // EnemyManager 초기화 - 적 생성을 위해
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }

    // 메타버스 씬에서 점수 로드
    private void Start()
    {
        score = PlayerPrefs.GetInt(MiniGameScoreKey, 0);
        bestScore = PlayerPrefs.GetInt(MiniGameBestScoreKey, 0);
        SetMiniGameScoreUI();

        if (!isFirstLoading)
        {
            StartGame(); // 두 번째 이후 씬 로딩 시 자동 시작
        }
        else
        {
            isFirstLoading = false; // 첫 로딩 해제
        }
    }

    // 미니게임 영역 진입시 Popup On
    public void EnterMiniGameZone()
    {
        uiManager.ChangeState(UIState.Popup);
    }

    // 미니게임 영역 이탈시 Score On
    public void ExitMiniGameZone()
    {
        uiManager.ChangeState(UIState.Score);
        SetMiniGameScoreUI();
    }

    // 미니게임 실행 - Popup start button 선택시 호출
    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }

    // 미니게임 종료 후 Score UI 반영
    public void SetMiniGameScoreUI()
    {
        UpdateMiniGameScore();
        uiManager.SetScoreUI();
    }

    // 미니게임 점수 갱신 확인
    private void UpdateMiniGameScore()
    {
        if (bestScore < score)
        {
            Debug.Log("최고 점수 갱신");
            bestScore = score;
            PlayerPrefs.SetInt(MiniGameBestScoreKey, bestScore);
            PlayerPrefs.Save();
        }
    }

    // 맵 게임 시작
    public void StartGame()
    {
        StartNextWave();
    }
    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // 끝났으면 다음 웨이브 진행
    public void EndOfWave()
    {
        StartNextWave();
    }

    // 종료
    public void GameOver()
    {
        enemyManager.StopWave();
    }
}

