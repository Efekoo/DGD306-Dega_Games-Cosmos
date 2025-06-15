using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip mainMenuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip CreditsMusic;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
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

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => PlayMusicForScene();
    }

    void PlayMusicForScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;
            case "Credits":
                PlayMusic(CreditsMusic);
                break;
            case "Level1":
                PlayMusic(level1Music);
                break;
            case "Level1Boss":
                PlayMusic(level1Music);
                break;
            case "Level2":
                PlayMusic(level2Music);
                break;
            case "Level2Boss":
                PlayMusic(level2Music);
                break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}