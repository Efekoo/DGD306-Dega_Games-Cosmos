using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLevel2 : MonoBehaviour
{
    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Oyun kapatýlýyor");
        Application.Quit();
    }

}
