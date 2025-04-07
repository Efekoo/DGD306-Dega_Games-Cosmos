using UnityEngine;

public class Level1Spawner : MonoBehaviour
{
    public GameObject[] characterPrefabs; // 0: Gemi 1, 1: Gemi 2
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        Debug.Log("LEVEL1SPAWNER �ALI�TI");

        Debug.Log("Player1 Index: " + PlayerSelectionData.player1Index);
        Debug.Log("Player2 Index: " + PlayerSelectionData.player2Index);
        Debug.Log("Co-op mu? " + PlayerSelectionData.isCoop);

        if (PlayerSelectionData.player1Index < 0 || PlayerSelectionData.player1Index >= characterPrefabs.Length)
        {
            Debug.LogError("Hatal� Player1 Index! Prefab dizisi dolu mu?");
            return;
        }

        Vector3 spawnPos1 = player1SpawnPoint.position;
        spawnPos1.z = 0f;

        GameObject p1 = Instantiate(characterPrefabs[PlayerSelectionData.player1Index], spawnPos1, Quaternion.identity);
        Debug.Log("Player 1 gemisi olu�turuldu: " + p1.name);

        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatal� Player2 Index! Prefab dizisi dolu mu?");
                return;
            }

            Vector3 spawnPos2 = player2SpawnPoint.position;
            spawnPos2.z = 0f;

            GameObject p2 = Instantiate(characterPrefabs[PlayerSelectionData.player2Index], spawnPos2, Quaternion.identity);
            Debug.Log("Player 2 gemisi olu�turuldu: " + p2.name);
        }
    }
}