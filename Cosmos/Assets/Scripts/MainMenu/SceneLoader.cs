using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadCharacterSelectScene()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
}