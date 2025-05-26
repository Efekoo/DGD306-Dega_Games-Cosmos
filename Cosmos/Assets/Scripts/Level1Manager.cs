using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;

    public GameObject tryAgainPanel;
    private int deadPlayers = 0;

    private int enemiesDestroyed = 0;
    public int enemiesToWin = 10; // Kaç düşman vurunca geçileceğini buradan ayarla

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

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    // ✅ Yeni eklenen kısım
    public void OnEnemyDestroyed()
    {
        enemiesDestroyed++;

        if (enemiesDestroyed >= enemiesToWin)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}