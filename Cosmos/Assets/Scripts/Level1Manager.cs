using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;

    public GameObject tryAgainPanel;
    private int deadPlayers = 0;

    private int enemiesDestroyed = 0;
    public int enemiesToWin = 20;

    void Awake()
    {
        Instance = this;
        tryAgainPanel.SetActive(false);
    }

    public void OnPlayerDied()
    {
        deadPlayers++;

        bool isCoop = PlayerSelectionData.isCoop;

        if ((isCoop && deadPlayers >= 2) || (!isCoop && deadPlayers >= 1))
        {
            Time.timeScale = 0f;
            tryAgainPanel.SetActive(true);
        }
    }

   

   
    public void OnEnemyDestroyed()
    {
        enemiesDestroyed++;

        if (enemiesDestroyed >= enemiesToWin)
        {
            SceneManager.LoadScene("Level1Boss");
        }
    }
}