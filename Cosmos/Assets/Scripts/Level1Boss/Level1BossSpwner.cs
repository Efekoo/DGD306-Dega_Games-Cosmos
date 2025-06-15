using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Level1SBossSpwner : MonoBehaviour 
{
    public GameObject[] characterPrefabs;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        Sprite[] healthSprites = LoadHealthSprites();
        Sprite[] overheatSprites = LoadOverheatSprites();

        if (PlayerSelectionData.player1Index < 0 || PlayerSelectionData.player1Index >= characterPrefabs.Length)
        {
            Debug.LogError("Hatalý Player1 Index!");
            return;
        }

        PlayerInput p1Input = PlayerInput.Instantiate(
            characterPrefabs[PlayerSelectionData.player1Index]


        );
        GameObject p1 = p1Input.gameObject;
        p1.transform.position = player1SpawnPoint.position;

        PlayerMovement p1Move = p1.GetComponent<PlayerMovement>();
        p1Move.isPlayerOne = true;
        p1Move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
        p1Move.healthSprites = healthSprites;

        PlayerShooting p1Shooting = p1.GetComponent<PlayerShooting>();
        p1Shooting.Init(
            true,
            GameObject.Find("P1OverheatBarImage").GetComponent<Image>(),
            overheatSprites
        );

        if (PlayerSelectionData.isCoop)
        {
            if (PlayerSelectionData.player2Index < 0 || PlayerSelectionData.player2Index >= characterPrefabs.Length)
            {
                Debug.LogError("Hatalý Player2 Index!");
                return;
            }

            if (Gamepad.all.Count > 1)
            {
                PlayerInput p2Input = PlayerInput.Instantiate(
                    characterPrefabs[PlayerSelectionData.player2Index]


                );
                GameObject p2 = p2Input.gameObject;
                p2.transform.position = player2SpawnPoint.position;

                PlayerMovement p2Move = p2.GetComponent<PlayerMovement>();
                p2Move.isPlayerOne = false;
                p2Move.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
                p2Move.healthSprites = healthSprites;

                PlayerShooting p2Shooting = p2.GetComponent<PlayerShooting>();
                p2Shooting.Init(
                    false,
                    GameObject.Find("P2OverheatBarImage").GetComponent<Image>(),
                    overheatSprites
                );
            }
            else
            {
                Debug.LogWarning("Gamepad bulunamadý. Co-op çalýþmaz.");
            }
        }
        else
        {
            GameObject p2Bar = GameObject.Find("P2HealthBarImage");
            if (p2Bar != null) p2Bar.SetActive(false);

            GameObject p2Overheat = GameObject.Find("P2OverheatBarImage");
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

    private Sprite[] LoadOverheatSprites()
    {
        Sprite[] sprites = new Sprite[6];
        for (int i = 0; i <= 5; i++)
        {
            sprites[i] = Resources.Load<Sprite>("OverheatSprites/" + i);
        }
        return sprites;
    }
}