using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public int meteorDestroyed = 0;
    public int meteorEscaped = 0;

    public GameObject tryAgainPanel;

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

    public void RetryTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }
}