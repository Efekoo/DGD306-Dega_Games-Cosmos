using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLevel1 : MonoBehaviour
{
    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Oyun kapatılıyor");
        Application.Quit();
    }

}
