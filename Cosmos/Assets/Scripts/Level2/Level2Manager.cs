using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;

    public GameObject tryAgainPanel;
    private int deadPlayers = 0;

    void Awake()
    {
        Instance = this;
        tryAgainPanel?.SetActive(false);
    }

    public void OnPlayerDied()
    {
        deadPlayers++;

        bool isCoop = PlayerSelectionData.isCoop;

        if ((isCoop && deadPlayers >= 2) || (!isCoop && deadPlayers >= 1))
        {
            Time.timeScale = 0f;
            tryAgainPanel?.SetActive(true);
        }
    }

}