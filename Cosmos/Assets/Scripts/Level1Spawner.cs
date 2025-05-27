using UnityEngine;
using UnityEngine.InputSystem;
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


        PlayerInput p1Input = PlayerInput.Instantiate(
        characterPrefabs[PlayerSelectionData.player1Index],
        controlScheme: "Gamepad",
        pairWithDevice: Gamepad.all[0]
        );
        GameObject p1 = p1Input.gameObject;
        p1.transform.position = player1SpawnPoint.position;

        PlayerMovement p1Move = p1.GetComponent<PlayerMovement>();
        p1Move.isPlayerOne = true;
        p1Move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
        p1Move.healthSprites = LoadHealthSprites();

        PlayerShooting p1Shooting = p1.GetComponent<PlayerShooting>();
        p1Shooting.isPlayerOne = true;
        p1Shooting.overheatSlider = GameObject.Find("P1OverheatSlider").GetComponent<Slider>();

        // 🎮 Co-op varsa Player 2'yi de ekle
        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatalı Player2 Index!");
                return;
            }

            // Eğer en az bir gamepad varsa, Player 2'ye bağla
            if (Gamepad.all.Count > 0)
            {

                PlayerInput p2Input = PlayerInput.Instantiate(
                characterPrefabs[PlayerSelectionData.player2Index],
                controlScheme: "Gamepad",
                pairWithDevice: Gamepad.all[1]
            );
                GameObject p2 = p2Input.gameObject;
                p2.transform.position = player2SpawnPoint.position;

                PlayerMovement p2Move = p2.GetComponent<PlayerMovement>();
                p2Move.isPlayerOne = false;
                p2Move.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
                p2Move.healthSprites = LoadHealthSprites();

                PlayerShooting p2Shooting = p2.GetComponent<PlayerShooting>();
                p2Shooting.isPlayerOne = false;
                p2Shooting.overheatSlider = GameObject.Find("P2OverheatSlider").GetComponent<Slider>();
            }
            else
            {
                Debug.LogWarning("Gamepad bulunamadı. Co-op çalışmaz.");
            }
        }
        else
        {
            // Tek kişilikte P2 UI gizle
            GameObject p2Bar = GameObject.Find("P2HealthBarImage");
            if (p2Bar != null) p2Bar.SetActive(false);

            GameObject p2Overheat = GameObject.Find("P2OverheatSlider");
            if (p2Overheat != null) p2Overheat.SetActive(false);
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