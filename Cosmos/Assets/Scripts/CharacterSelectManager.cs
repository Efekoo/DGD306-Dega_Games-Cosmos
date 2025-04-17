using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectManager : MonoBehaviour
{
    public GameObject highlightP1;
    public TMP_Text instructionText;

    void Start()
    {
        instructionText.text = "PLAYER 1:\n[1] Gemi 1  |  [2] Gemi 2\n[T] Tek başına oyna  |  [Y] Co-op (2. oyuncuya kalan gemi verilir)";
        highlightP1.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerSelectionData.player1Index = 0;
            highlightP1.SetActive(true);
            highlightP1.transform.position = GameObject.Find("Character1Image").transform.position;
            Debug.Log("Player 1 seçimi: Gemi 1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerSelectionData.player1Index = 1;
            highlightP1.SetActive(true);
            highlightP1.transform.position = GameObject.Find("Character2Image").transform.position;
            Debug.Log("Player 1 seçimi: Gemi 2");
        }

        
        if (PlayerSelectionData.player1Index != -1)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                PlayerSelectionData.isCoop = false;
                PlayerSelectionData.player2Index = -1;
                SceneManager.LoadScene("Tutorial");
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                PlayerSelectionData.isCoop = true;
                PlayerSelectionData.player2Index = (PlayerSelectionData.player1Index == 0) ? 1 : 0;
                SceneManager.LoadScene("Tutorial");
            }
        }
    }
}