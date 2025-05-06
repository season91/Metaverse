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

    // Wave 게임 관련 변수
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;

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

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }

    private void Start()
    {
        score = PlayerPrefs.GetInt(MiniGameScoreKey, 0);
        bestScore = PlayerPrefs.GetInt(MiniGameBestScoreKey, 0);
        //SetMiniGameScoreUI();

        // 씬 이동시 사용하기 위해 
        DontDestroyOnLoad(this.gameObject);
    }

    // 미니게임 영역 진입시 Popup On
    public void EnterMiniGameZone()
    {
        uiManager.ChangeState(UIState.Popup);
    }


    // 미니게임 영역 이탈시 Score On
    //public void ExitMiniGameZone()
    //{
    //    uiManager.ChangeState(UIState.Score);
    //    SetMiniGameScoreUI();
    //}

    //미니게임 실행 - Popup start button 선택시 호출

    //// 미니게임 종료 후 Score UI 반영
    //public void SetMiniGameScoreUI()
    //{
    //    UpdateMiniGameScore();
    //    uiManager.SetScoreUI();
    //}

    // 미니게임 점수 갱신 확인
    //private void UpdateMiniGameScore()
    //{
    //    if (bestScore < score)
    //    {
    //        Debug.Log("최고 점수 갱신");
    //        bestScore = score;
    //        PlayerPrefs.SetInt(MiniGameBestScoreKey, bestScore);
    //        PlayerPrefs.Save();
    //    }
    //}


    // 미니게임 시작
    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }
    // 맵 게임 시작
    public void StartWaveGame()
    {
        SceneManager.LoadScene("Dunjeon");
        StartNextWave();
    }
    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }
    public void EndOfWave()
    {
        StartNextWave();
    }
    
    public void GameOver()
    {
        enemyManager.StopWave();
    }
}

