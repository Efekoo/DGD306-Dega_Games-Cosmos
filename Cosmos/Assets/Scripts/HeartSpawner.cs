using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab;
    public float spawnInterval = 10f;
    public float minX = -8f, maxX = 8f;
    public float minY = -4.65f, maxY = 3.24f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnHeart();
            timer = 0f;
        }
    }

    void SpawnHeart()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0f);

        Instantiate(heartPrefab, spawnPos, Quaternion.identity);
    }
}