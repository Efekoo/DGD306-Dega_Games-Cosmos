using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

 class GameManager : MonoBehaviour
{
    public Text winText;
    private int totalEnemies;
    private int enemiesDestroyed;

    void Start()
    {
        
        totalEnemies = Object.FindObjectsByType<Destructable>(FindObjectsSortMode.None).Length;
        enemiesDestroyed = 0;

        winText.gameObject.SetActive(false);
    }

    public void EnemyDestroyed()
    {
        enemiesDestroyed++;

        if (enemiesDestroyed >= totalEnemies)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        winText.gameObject.SetActive(true);
        Invoke("GoToMenu", 2f);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
