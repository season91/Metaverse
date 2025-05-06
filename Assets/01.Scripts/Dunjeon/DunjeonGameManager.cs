using UnityEngine;
using UnityEngine.SceneManagement;

public class DunjeonGameManager : MonoBehaviour
{
    // Player Resource 설정을 위한 호출
    private ResourceController _playerResourceController;

    public GameObject player;

    // 적 생성을 위해 호출
    private EnemyManager enemyManager;
    public static bool isFirstLoading = true; // 첫 로딩인지 구분

    // 적 웨이브 게임 관련 변수
    [SerializeField] private int currentWaveIndex = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        // EnemyManager 초기화 - 적 생성을 위해
        enemyManager = GetComponentInChildren<EnemyManager>();
        if (enemyManager != null) enemyManager.Init(this);

        // Player Resource 설정
        // 체력 변경 이벤트를 UI에 연결 : 리소스컨트롤러가 알아서 일 하다가 체력 변경 이벤트 발생시
        _playerResourceController = player.GetComponent<ResourceController>();
        // 중복 등록 방지를 위해 먼저 제거한 뒤 
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        // 다시 등록
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }

    private void Start()
    {
        StartWaveGame();
    }

    private void Update()
    {
        
    }

    public void StartWaveGame()
    {
        //uiManager.SetPlayGame(); // UI 상태를 게임 상태로 전환
        StartNextWave();
    }


    private void StartNextWave()
    {
        Debug.Log("wave " + (1 + currentWaveIndex / 5));
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // 끝났으면 다음 웨이브 진행
    public void EndOfWave()
    {
        StartNextWave();
    }

    // 종료
    public void WaveGameOver()
    {
        enemyManager.StopWave();
    }
}
