using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
   public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
