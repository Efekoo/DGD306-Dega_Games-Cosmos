using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Animator animator;

    public float openDuration = 6.1f;
    public float closedDuration = 3f;

    private bool isOpen = false;
    private float timer;
    private bool isDead = false;

    [Header("Boss Health Bar")]
    [SerializeField] private Image bossHealthBarImage;
    [SerializeField] private Sprite[] bossHealthSprites;

    [Header("Cutscene")]
    [SerializeField] private GameObject cutscenePanel;
    [SerializeField] private VideoPlayer cutsceneVideoPlayer;

    void Start()
    {
        bossHealthSprites = LoadHealthSprites();
        currentHealth = maxHealth;
        UpdateHealthBar();
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
        UpdateHealthBar();
        Debug.Log($"Boss hasar aldı! Kalan can: {currentHealth}");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    void UpdateHealthBar()
    {
        if (bossHealthBarImage == null || bossHealthSprites == null || bossHealthSprites.Length == 0)
            return;

        int spriteIndex = Mathf.Clamp(currentHealth, 0, bossHealthSprites.Length - 1);
        bossHealthBarImage.sprite = bossHealthSprites[spriteIndex];
    }
    private Sprite[] LoadHealthSprites()
    {
        Sprite[] sprites = new Sprite[21];
        for (int i = 0; i <= 20; i++)
        {
            sprites[i] = Resources.Load<Sprite>("BossHealthSprites/" + i);
        }
        return sprites;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Boss öldü, cutscene sahnesi açılıyor.");

        SceneManager.LoadScene("CutsceneScene");
    }
}