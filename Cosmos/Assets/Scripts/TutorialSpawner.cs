using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        // Player 1 spawn
        Vector3 spawnPos1 = player1SpawnPoint.position;
        spawnPos1.z = 0f;

        GameObject p1 = Instantiate(characterPrefabs[PlayerSelectionData.player1Index], spawnPos1, Quaternion.identity);

        // Player 1 hareket kontrolü
        PlayerMovement p1Move = p1.GetComponent<PlayerMovement>();
        p1Move.isPlayerOne = true;
        p1Move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
        p1Move.healthSprites = LoadHealthSprites();

        // Player 1 ateş kontrolü
        PlayerShooting p1Shooting = p1.GetComponent<PlayerShooting>();
        p1Shooting.isPlayerOne = true; // 🔥 Burası eklendi
        Slider p1Slider = GameObject.Find("P1OverheatSlider").GetComponent<Slider>();
        p1Shooting.overheatSlider = p1Slider;

        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatalı Player2 Index!");
                return;
            }

            // Player 2 spawn
            Vector3 spawnPos2 = player2SpawnPoint.position;
            spawnPos2.z = 0f;

            GameObject p2 = Instantiate(characterPrefabs[PlayerSelectionData.player2Index], spawnPos2, Quaternion.identity);

            // Player 2 hareket kontrolü
            PlayerMovement p2Move = p2.GetComponent<PlayerMovement>();
            p2Move.isPlayerOne = false;
            p2Move.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
            p2Move.healthSprites = LoadHealthSprites();

            // Player 2 ateş kontrolü
            PlayerShooting p2Shooting = p2.GetComponent<PlayerShooting>();
            p2Shooting.isPlayerOne = false; // 🔥 Burası eklendi
            Slider p2Slider = GameObject.Find("P2OverheatSlider").GetComponent<Slider>();
            p2Shooting.overheatSlider = p2Slider;
        }
        else
        {
            // Co-op değilse Player 2'nin UI elementlerini gizle
            GameObject p2Bar = GameObject.Find("P2HealthBarImage");
            if (p2Bar != null)
            {
                p2Bar.SetActive(false);
            }

            GameObject p2Overheat = GameObject.Find("P2OverheatSlider");
            if (p2Overheat != null)
            {
                p2Overheat.SetActive(false);
            }
        }
    }

    private Sprite[] LoadHealthSprites()
    {
        Sprite[] sprites = new Sprite[6];
        for (int i = 0; i <= 5; i++)
        {
            sprites[i] = Resources.Load<Sprite>("HealthSprites/" + i);
        }
        return sprites;
    }
}