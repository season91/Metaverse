using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // 생성하고자 하는 적 프리팹 목록
    [SerializeField] private List<Rect> spawnAreas; // 적을 생성할 영역 리스트
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상
    private List<EnemyController> activeEnemies = new List<EnemyController>(); // 생성된 적 목록

    private GameManager gameManager;
    private UIManager uiManager;

    // 웨이브 관련 변수
    private Coroutine waveRoutine;
    private bool enemySpawnComplite;
    [SerializeField] private float timeBetweenWaves = 1f; // 다음 웨이브 실행 전 일정 시간 대기를 위함
    [SerializeField] private float timeBetweenSpawns = 0.2f;  // 적 생성하고 다음 생성 전 일정 시간 대기를 위함
    public int killCount = 0;

    // GameManager 에서 초기화하여 호출
    public void Init(GameManager gameManager, UIManager uiManager)
    {
        this.gameManager = gameManager;
        this.uiManager = uiManager;
    }

    // 적 랜덤 위치에 생성
    private void SpawnRandomEnemy(int count)
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            return;
        }

        // 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        // Rect 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );
        // 적 생성
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        // 생성된 적 목록에 추가
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
        enemyController.Init(gameManager.Player.transform, this); // 유저 위치 = 적 입장에선 공격 대상 위치

        if(count == 0)
        {
            enemyController.RemoveKillChangeEvent(uiManager.ChangeKill);
            enemyController.AddKillChangeEvent(uiManager.ChangeKill);
        }
        activeEnemies.Add(enemyController);
    }

    // 개발 테스트를 위해 기즈모 영역 시각화
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    // 적 사망시 생성된 적 목록에서 제거, 다음 웨이브 진행
    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            gameManager.EndOfWave();
    }

    // 웨이브 시작
    public void StartWave(int waveCount)
    {
        // waveCount가 0으로 온다면 바로 GameManager에게 해당 웨이브 종료처리 보낼 것
        // GameManager의 EndOfWave에서 NextWave 처리하기 때문
        // 왜 EnemyManager에서 안하는지?
        if (waveCount <= 0)
        {
            gameManager.EndOfWave();
            return;
        }

        // 웨이브 루틴 있는지 확인
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);

        // 코루틴 실행 StartCoroutine
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        // 일정 시간 대기
        yield return new WaitForSeconds(timeBetweenWaves);

        // 대기할 동안 반복문 실행
        // 웨이브 개수만큼 반복 - 생성하고 기다리고 생성하고 기다리고
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy(i);
        }
        // 생성 완료
        enemySpawnComplite = true;

    }

    // 웨이브 종료
    public void StopWave()
    {
        StopAllCoroutines();
    }

}
