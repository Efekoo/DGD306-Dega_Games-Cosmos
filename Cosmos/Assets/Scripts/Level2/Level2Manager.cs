using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;

    public GameObject tryAgainPanel;
    private int deadPlayers = 0;

    [Header("Level Bitirme")]
    public int enemiesDestroyed = 0;
    public int targetEnemiesToDestroy = 30;

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

    public void OnEnemyDestroyed()
    {
        enemiesDestroyed++;
        Debug.Log("Düþman sayýsý: " + enemiesDestroyed);

        if (enemiesDestroyed >= targetEnemiesToDestroy)
        {
            Debug.Log("Level2 tamamlandý!");
            SceneManager.LoadScene("Level2Boss");
        }
    }
}