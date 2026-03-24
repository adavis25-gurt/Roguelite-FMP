using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform player;
    [SerializeField] Timer timer;

    [SerializeField] float spawnRadius = 20f;
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float minSpawnInterval = 0.5f;
    [SerializeField] float intervalReductionPerMinute = 0.2f;

    [SerializeField] int baseSpawnCount = 1;
    [SerializeField] float spawnCountExponent = 2.5f;

    float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        float interval = Mathf.Max(minSpawnInterval, spawnInterval - (timer.minutes * intervalReductionPerMinute));

        if (spawnTimer >= interval)
        {
            spawnTimer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        float t = timer.minutes + (timer.seconds / 60f);
        int count = Mathf.FloorToInt(baseSpawnCount * Mathf.Pow(t + 1f, spawnCountExponent));
        for (int i = 0; i < count; i++)
            SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPos = player.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if (stats != null) stats.timer = timer;

        EnemyAttacking attacking = enemy.GetComponent<EnemyAttacking>();
        if (attacking != null) attacking.SetTarget(player.gameObject);
    }
}