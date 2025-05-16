using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        if (PlayerSelectionData.player1Index < 0 || PlayerSelectionData.player1Index >= characterPrefabs.Length)
        {
            Debug.LogError("Hatalı Player1 Index!");
            return;
        }

        Vector3 spawnPos1 = player1SpawnPoint.position;
        spawnPos1.z = 0f;

        GameObject p1 = Instantiate(characterPrefabs[PlayerSelectionData.player1Index], spawnPos1, Quaternion.identity);
        p1.GetComponent<PlayerShooting>().isPlayerOne = true;

        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatalı Player2 Index!");
                return;
            }

            Vector3 spawnPos2 = player2SpawnPoint.position;
            spawnPos2.z = 0f;

            GameObject p2 = Instantiate(characterPrefabs[PlayerSelectionData.player2Index], spawnPos2, Quaternion.identity);
            p2.GetComponent<PlayerShooting>().isPlayerOne = false;
        }
    }
}