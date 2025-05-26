using UnityEngine;
using UnityEngine.UI;

public class Level2Spawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        if (PlayerSelectionData.player1Index >= 0)
        {
            GameObject p1 = Instantiate(characterPrefabs[PlayerSelectionData.player1Index], player1SpawnPoint.position, Quaternion.identity);
            PlayerMovement move = p1.GetComponent<PlayerMovement>();
            move.isPlayerOne = true;
            move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
            move.healthSprites = LoadHealthSprites(); // 💡 EKLENDİ

            PlayerShooting shoot = p1.GetComponent<PlayerShooting>();
            shoot.isPlayerOne = true;
            shoot.overheatSlider = GameObject.Find("P1OverheatSlider")?.GetComponent<Slider>();
        }

        if (PlayerSelectionData.isCoop && PlayerSelectionData.player2Index >= 0)
        {
            GameObject p2 = Instantiate(characterPrefabs[PlayerSelectionData.player2Index], player2SpawnPoint.position, Quaternion.identity);
            PlayerMovement move2 = p2.GetComponent<PlayerMovement>();
            move2.isPlayerOne = false;
            move2.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
            move2.healthSprites = LoadHealthSprites(); // 💡 EKLENDİ

            PlayerShooting shoot2 = p2.GetComponent<PlayerShooting>();
            shoot2.isPlayerOne = false;
            shoot2.overheatSlider = GameObject.Find("P2OverheatSlider")?.GetComponent<Slider>();
        }
    }

    // 💡 Sprite dizisini Resources klasöründen yükleyen fonksiyon
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