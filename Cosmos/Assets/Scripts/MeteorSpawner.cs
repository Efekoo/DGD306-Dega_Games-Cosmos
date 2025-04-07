using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject smallMeteorPrefab;
    public GameObject bigMeteorPrefab;
    public float spawnInterval = 1.5f;
    public float minY = -4.5f;
    public float maxY = 4.5f;
    public float minX = -4.5f;
    public float maxX = 4.5f;


    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnSmallMeteor();
            SpawnBigMeteor();
            timer = 0f;
        }
    }

    void SpawnSmallMeteor()
    {
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        Instantiate(smallMeteorPrefab, spawnPos, Quaternion.identity);
    }
    void SpawnBigMeteor()
    {
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        Instantiate(bigMeteorPrefab, spawnPos, Quaternion.identity);
    }
}