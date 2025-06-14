using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverLevel2Boss : MonoBehaviour
{

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2Boss");
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