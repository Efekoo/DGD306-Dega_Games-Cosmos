using UnityEngine;
using UnityEngine.UI;

public class Level1Spawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        Debug.Log("LEVEL1SPAWNER ÇALIÞTI");

        Debug.Log("Player1 Index: " + PlayerSelectionData.player1Index);
        Debug.Log("Player2 Index: " + PlayerSelectionData.player2Index);
        Debug.Log("Co-op mu? " + PlayerSelectionData.isCoop);

        if (PlayerSelectionData.player1Index < 0 || PlayerSelectionData.player1Index >= characterPrefabs.Length)
        {
            Debug.LogError("Hatalý Player1 Index! Prefab dizisi dolu mu?");
            return;
        }

        Vector3 spawnPos1 = player1SpawnPoint.position;
        spawnPos1.z = 0f;

        GameObject p1 = Instantiate(characterPrefabs[PlayerSelectionData.player1Index], spawnPos1, Quaternion.identity);
        Debug.Log("Player 1 gemisi oluþturuldu: " + p1.name);

        PlayerMovement p1Move = p1.GetComponent<PlayerMovement>();
        p1Move.isPlayerOne = true;
        p1Move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
        p1Move.healthSprites = LoadHealthSprites();

        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatalý Player2 Index! Prefab dizisi dolu mu?");
                return;
            }

            Vector3 spawnPos2 = player2SpawnPoint.position;
            spawnPos2.z = 0f;

            GameObject p2 = Instantiate(characterPrefabs[PlayerSelectionData.player2Index], spawnPos2, Quaternion.identity);
            Debug.Log("Player 2 gemisi oluþturuldu: " + p2.name);

            PlayerMovement p2Move = p2.GetComponent<PlayerMovement>();
            p2Move.isPlayerOne = false;
            p2Move.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
            p2Move.healthSprites = LoadHealthSprites();
        }
        if (!PlayerSelectionData.isCoop)
        {
            GameObject p2Bar = GameObject.Find("P2HealthBarImage");
            if (p2Bar != null)
            {
                p2Bar.SetActive(false);
            }
        }
    }

    private Sprite[] LoadHealthSprites()
    {
        Sprite[] sprites = new Sprite[6];
        for (int i = 0; i <= 5; i++)
        {
            sprites[i] = Resources.Load<Sprite>("HealthSprites/" + i);
            Debug.Log("Sprite " + i + ": " + (sprites[i] != null ? "Yüklendi" : "YÜKLENEMEDÝ"));
        }
        return sprites;
    }
}