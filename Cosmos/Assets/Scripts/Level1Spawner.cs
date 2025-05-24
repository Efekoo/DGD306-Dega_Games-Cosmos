using UnityEngine;
using UnityEngine.UI;

public class Level1Spawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        Debug.Log("LEVEL1SPAWNER ÇALIŞTI");
        Debug.Log("Player1 Index: " + PlayerSelectionData.player1Index);
        Debug.Log("Player2 Index: " + PlayerSelectionData.player2Index);
        Debug.Log("Co-op mu? " + PlayerSelectionData.isCoop);

        if (PlayerSelectionData.player1Index < 0 || PlayerSelectionData.player1Index >= characterPrefabs.Length)
        {
            Debug.LogError("Hatalı Player1 Index! Prefab dizisi dolu mu?");
            return;
        }

        // Player 1 spawn
        Vector3 spawnPos1 = player1SpawnPoint.position;
        spawnPos1.z = 0f;

        GameObject p1 = Instantiate(characterPrefabs[PlayerSelectionData.player1Index], spawnPos1, Quaternion.identity);
        Debug.Log("Player 1 gemisi oluşturuldu: " + p1.name);

        PlayerMovement p1Move = p1.GetComponent<PlayerMovement>();
        p1Move.isPlayerOne = true;
        p1Move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
        p1Move.healthSprites = LoadHealthSprites();

        PlayerShooting p1Shooting = p1.GetComponent<PlayerShooting>();
        p1Shooting.isPlayerOne = true; // 🔥 Eklenen kısım
        Slider p1Slider = GameObject.Find("P1OverheatSlider").GetComponent<Slider>();
        p1Shooting.overheatSlider = p1Slider;

        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatalı Player2 Index! Prefab dizisi dolu mu?");
                return;
            }

            // Player 2 spawn
            Vector3 spawnPos2 = player2SpawnPoint.position;
            spawnPos2.z = 0f;

            GameObject p2 = Instantiate(characterPrefabs[PlayerSelectionData.player2Index], spawnPos2, Quaternion.identity);
            Debug.Log("Player 2 gemisi oluşturuldu: " + p2.name);

            PlayerMovement p2Move = p2.GetComponent<PlayerMovement>();
            p2Move.isPlayerOne = false;
            p2Move.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
            p2Move.healthSprites = LoadHealthSprites();

            PlayerShooting p2Shooting = p2.GetComponent<PlayerShooting>();
            p2Shooting.isPlayerOne = false; // 🔥 Eklenen kısım
            Slider p2Slider = GameObject.Find("P2OverheatSlider").GetComponent<Slider>();
            p2Shooting.overheatSlider = p2Slider;
        }
        else
        {
            GameObject p2Bar = GameObject.Find("P2HealthBarImage");
            if (p2Bar != null)
            {
                p2Bar.SetActive(false);
            }

            GameObject p2SliderObj = GameObject.Find("P2OverheatSlider");
            if (p2SliderObj != null)
            {
                p2SliderObj.SetActive(false);
            }
        }
    }

    private Sprite[] LoadHealthSprites()
    {
        Sprite[] sprites = new Sprite[6];
        for (int i = 0; i <= 5; i++)
        {
            sprites[i] = Resources.Load<Sprite>("HealthSprites/" + i);
            Debug.Log("Sprite " + i + ": " + (sprites[i] != null ? "Yüklendi" : "YÜKLENEMEDİ"));
        }
        return sprites;
    }
}