using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public int meteorDestroyed = 0;
    public int meteorEscaped = 0;

    public GameObject tryAgainPanel;

    private int deadPlayers = 0;

    void Awake()
    {
        Instance = this;
        tryAgainPanel.SetActive(false);
    }

    public void OnMeteorDestroyed()
    {
        meteorDestroyed++;

        if (meteorDestroyed >= 5)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void OnMeteorEscaped()
    {
        meteorEscaped++;

        if (meteorEscaped >= 5)
        {
            Time.timeScale = 0f;
            tryAgainPanel.SetActive(true);
        }
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

    public void RetryTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }
}