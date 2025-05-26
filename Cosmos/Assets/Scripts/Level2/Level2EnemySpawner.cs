using UnityEngine;

public class Level2EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // enemyPlane + AlienEnemyUFO
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
        int index = Random.Range(0, enemyPrefabs.Length);
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(spawnX, y, 0f);

        Instantiate(enemyPrefabs[index], pos, Quaternion.identity);
    }
}