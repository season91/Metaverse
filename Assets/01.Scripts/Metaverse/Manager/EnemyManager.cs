using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // �����ϰ��� �ϴ� �� ������ ���
    [SerializeField] private List<Rect> spawnAreas; // ���� ������ ���� ����Ʈ
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����
    private List<EnemyController> activeEnemies = new List<EnemyController>(); // ������ �� ���

    private GameManager gameManager;
    private UIManager uiManager;

    // ���̺� ���� ����
    private Coroutine waveRoutine;
    private bool enemySpawnComplite;
    [SerializeField] private float timeBetweenWaves = 1f; // ���� ���̺� ���� �� ���� �ð� ��⸦ ����
    [SerializeField] private float timeBetweenSpawns = 0.2f;  // �� �����ϰ� ���� ���� �� ���� �ð� ��⸦ ����
    public int killCount = 0;

    // GameManager ���� �ʱ�ȭ�Ͽ� ȣ��
    public void Init(GameManager gameManager, UIManager uiManager)
    {
        this.gameManager = gameManager;
        this.uiManager = uiManager;
    }

    // �� ���� ��ġ�� ����
    private void SpawnRandomEnemy(int count)
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            return;
        }

        // �� ������ ����
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        // Rect ���� ����
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect ���� ������ ���� ��ġ ���
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );
        // �� ����
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        // ������ �� ��Ͽ� �߰�
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
        enemyController.Init(gameManager.Player.transform, this); // ���� ��ġ = �� ���忡�� ���� ��� ��ġ

        if(count == 0)
        {
            enemyController.RemoveKillChangeEvent(uiManager.ChangeKill);
            enemyController.AddKillChangeEvent(uiManager.ChangeKill);
        }
        activeEnemies.Add(enemyController);
    }

    // ���� �׽�Ʈ�� ���� ����� ���� �ð�ȭ
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

    // �� ����� ������ �� ��Ͽ��� ����, ���� ���̺� ����
    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            gameManager.EndOfWave();
    }

    // ���̺� ����
    public void StartWave(int waveCount)
    {
        // waveCount�� 0���� �´ٸ� �ٷ� GameManager���� �ش� ���̺� ����ó�� ���� ��
        // GameManager�� EndOfWave���� NextWave ó���ϱ� ����
        // �� EnemyManager���� ���ϴ���?
        if (waveCount <= 0)
        {
            gameManager.EndOfWave();
            return;
        }

        // ���̺� ��ƾ �ִ��� Ȯ��
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);

        // �ڷ�ƾ ���� StartCoroutine
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        // ���� �ð� ���
        yield return new WaitForSeconds(timeBetweenWaves);

        // ����� ���� �ݺ��� ����
        // ���̺� ������ŭ �ݺ� - �����ϰ� ��ٸ��� �����ϰ� ��ٸ���
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy(i);
        }
        // ���� �Ϸ�
        enemySpawnComplite = true;

    }

    // ���̺� ����
    public void StopWave()
    {
        StopAllCoroutines();
    }

}
