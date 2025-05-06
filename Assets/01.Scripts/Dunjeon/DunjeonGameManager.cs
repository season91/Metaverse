using UnityEngine;
using UnityEngine.SceneManagement;

public class DunjeonGameManager : MonoBehaviour
{
    // Player Resource ������ ���� ȣ��
    private ResourceController _playerResourceController;

    public GameObject player;

    // �� ������ ���� ȣ��
    private EnemyManager enemyManager;
    public static bool isFirstLoading = true; // ù �ε����� ����

    // �� ���̺� ���� ���� ����
    [SerializeField] private int currentWaveIndex = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        // EnemyManager �ʱ�ȭ - �� ������ ����
        enemyManager = GetComponentInChildren<EnemyManager>();
        if (enemyManager != null) enemyManager.Init(this);

        // Player Resource ����
        // ü�� ���� �̺�Ʈ�� UI�� ���� : ���ҽ���Ʈ�ѷ��� �˾Ƽ� �� �ϴٰ� ü�� ���� �̺�Ʈ �߻���
        _playerResourceController = player.GetComponent<ResourceController>();
        // �ߺ� ��� ������ ���� ���� ������ �� 
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        // �ٽ� ���
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
        //uiManager.SetPlayGame(); // UI ���¸� ���� ���·� ��ȯ
        StartNextWave();
    }


    private void StartNextWave()
    {
        Debug.Log("wave " + (1 + currentWaveIndex / 5));
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // �������� ���� ���̺� ����
    public void EndOfWave()
    {
        StartNextWave();
    }

    // ����
    public void WaveGameOver()
    {
        enemyManager.StopWave();
    }
}
