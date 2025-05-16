using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 9f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int pointIndex = Random.Range(0, spawnPoints.Length);

        Vector3 spawnPos = spawnPoints[pointIndex].position;

        Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
    }
}