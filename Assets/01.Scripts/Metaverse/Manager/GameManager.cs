using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 처리
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // 체력변경
    private ResourceController _playerResourceController;

    // 몬스터 잡은수 변경
    private EnemyController _enemyController;

    // Player 이동, 바라보는 처리를 위한 호출
    public PlayerController Player {  get; private set; }

    // UI 전환을 위해 호출
    private UIManager uiManager;

    // Wave 게임 관련 변수
    [SerializeField] private int currentWaveIndex = 0;
    private EnemyManager enemyManager;

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

        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);

        // UIManager 초기화 - 전환을 위해
        uiManager = FindObjectOfType<UIManager>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this, uiManager);

        _playerResourceController = Player.GetComponent<ResourceController>();

        // 체력 변경 이벤트를 UI에 연결 : 리소스컨트롤러가 알아서 일 하다가 체력 변경 이벤트 발생시
        // 중복 등록 방지를 위해 먼저 제거한 뒤 
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        // 다시 등록
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

    }

    private void Start()
    {
        Score = PlayerPrefs.GetInt(MiniGameScoreKey, 0);
        BestScore = PlayerPrefs.GetInt(MiniGameBestScoreKey, 0);
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

