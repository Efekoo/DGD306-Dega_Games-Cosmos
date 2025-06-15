using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip mainMenuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip CreditsMusic;
    public AudioClip EndingMusic;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();


            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusicForScene();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene();
    }

    void PlayMusicForScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;
            case "Credits":
            case "Tutorial":
                PlayMusic(CreditsMusic);
                break;
            case "Intro":
            case "CutsceneScene":
                PlayMusic(EndingMusic);
                break;
            case "Level1":
            case "Level2":
                PlayMusic(level1Music);
                break;
            case "Level1Boss":
            case "Level2Boss":
                PlayMusic(level2Music);
                break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != null && audioSource.clip.name == clip.name) return;

        audioSource.clip = clip;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        audioSource.Play();
    }
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}