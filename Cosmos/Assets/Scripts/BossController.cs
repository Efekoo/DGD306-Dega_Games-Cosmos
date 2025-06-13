using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BossController : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Animator animator;

    public float openDuration = 6.1f;
    public float closedDuration = 3f;

    private bool isOpen = false;
    private float timer;
    private bool isDead = false;

    [Header("Cutscene")]
    [SerializeField] private GameObject cutscenePanel; // RawImage'ın olduğu panel
    [SerializeField] private VideoPlayer cutsceneVideoPlayer;

    void Start()
    {
        currentHealth = maxHealth;
        timer = 0f;
        SetClosedState();
    }

    void Update()
    {
        if (isDead) return;

        timer += Time.deltaTime;

        if (isOpen && timer >= openDuration)
        {
            SetClosedState();
        }
        else if (!isOpen && timer >= closedDuration)
        {
            SetOpenState();
        }
    }

    void SetOpenState()
    {
        isOpen = true;
        timer = 0f;
        animator.SetBool("isOpen", true);
    }

    void SetClosedState()
    {
        isOpen = false;
        timer = 0f;
        animator.SetBool("isOpen", false);
    }

    public void TakeDamage(int damage)
    {
        if (!isOpen || isDead)
        {
            Debug.Log("Boss şu an kapalı veya ölü, hasar ALMAZ.");
            return;
        }

        currentHealth -= damage;
        Debug.Log($"Boss hasar aldı! Kalan can: {currentHealth}");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Boss öldü, cutscene başlatılıyor.");
        StartCoroutine(BeginCutscene());
    }

    System.Collections.IEnumerator BeginCutscene()
    {
        cutscenePanel.SetActive(true);
        Debug.Log("Cutscene başladı");

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "cutscenemp.mp4");
        cutsceneVideoPlayer.url = path;


        cutsceneVideoPlayer.loopPointReached += OnCutsceneFinished;


        cutsceneVideoPlayer.Prepare();

        while (!cutsceneVideoPlayer.isPrepared)
        {
            yield return null;
        }

        cutsceneVideoPlayer.Play();
    }
    void OnCutsceneFinished(VideoPlayer vp)
    {
        Debug.Log("Cutscene tamamlandı, Level2'ye geçiliyor");
        SceneManager.LoadScene("Level2");
    }
}