using UnityEngine;
using UnityEngine.InputSystem;
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
            PlayerInput p1Input = PlayerInput.Instantiate(
                characterPrefabs[PlayerSelectionData.player1Index],
                controlScheme: "Gamepad",
                pairWithDevice: Gamepad.all[0]
            );
            GameObject p1 = p1Input.gameObject;
            p1.transform.position = player1SpawnPoint.position;

            PlayerMovement move = p1.GetComponent<PlayerMovement>();
            move.isPlayerOne = true;
            move.healthBarImage = GameObject.Find("P1HealthBarImage").GetComponent<Image>();
            move.healthSprites = LoadHealthSprites();

            PlayerShooting shoot = p1.GetComponent<PlayerShooting>();
            shoot.isPlayerOne = true;
            shoot.overheatSlider = GameObject.Find("P1OverheatSlider")?.GetComponent<Slider>();
        }


        if (PlayerSelectionData.isCoop && PlayerSelectionData.player2Index >= 0 && Gamepad.all.Count > 0)
        {
            PlayerInput p2Input = PlayerInput.Instantiate(
                characterPrefabs[PlayerSelectionData.player2Index],
                controlScheme: "Gamepad",
                pairWithDevice: Gamepad.all[1]
            );
            GameObject p2 = p2Input.gameObject;
            p2.transform.position = player2SpawnPoint.position;

            PlayerMovement move2 = p2.GetComponent<PlayerMovement>();
            move2.isPlayerOne = false;
            move2.healthBarImage = GameObject.Find("P2HealthBarImage").GetComponent<Image>();
            move2.healthSprites = LoadHealthSprites();

            PlayerShooting shoot2 = p2.GetComponent<PlayerShooting>();
            shoot2.isPlayerOne = false;
            shoot2.overheatSlider = GameObject.Find("P2OverheatSlider")?.GetComponent<Slider>();
        }
        else if (!PlayerSelectionData.isCoop)
        {

            GameObject.Find("P2HealthBarImage")?.SetActive(false);
            GameObject.Find("P2OverheatSlider")?.SetActive(false);
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