using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2BossManager : MonoBehaviour
{
    public static Level2BossManager Instance;

    public GameObject tryAgainPanel;
    private int deadPlayers = 0;

    [Header("Boss Takibi")]
    public GameObject boss;
    private bool levelCompleted = false;

    void Awake()
    {
        Instance = this;
        tryAgainPanel?.SetActive(false);
    }

    void Update()
    {
        if (!levelCompleted && boss == null)
        {
            levelCompleted = true;
            OnBossDefeated();
        }
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

    void OnBossDefeated()
    {
        Debug.Log("Boss yenildi! Level 2 tamamlandı!");

        SceneManager.LoadScene("Credits");
    }
}