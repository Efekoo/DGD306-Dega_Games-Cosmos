using UnityEngine;
using UnityEngine.UI;

public class Level2Boss : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Health Bar")]
    [SerializeField] private Image bossHealthBarImage;
    [SerializeField] private Sprite[] bossHealthSprites;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip loopingAmbienceClip;
    [SerializeField] private AudioClip missileFireClip;

    public GameObject missilePrefab;
    public Transform firePoint;
    public float fireInterval = 2f;

    private float fireTimer;

    void Start()
    {
        bossHealthSprites = LoadHealthSprites();
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (audioSource != null && loopingAmbienceClip != null)
        {
            audioSource.clip = loopingAmbienceClip;
            audioSource.loop = true;
            audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 0.8f);
            audioSource.Play();
        }
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            FireMissile();
            fireTimer = 0f;
        }
    }


    void FireMissile()
    {
        Instantiate(missilePrefab, firePoint.position, Quaternion.identity);

        if (audioSource != null && missileFireClip != null)
        {
            audioSource.PlayOneShot(missileFireClip);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
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
            sprites[i] = Resources.Load<Sprite>("Level2BossHealthSprites/" + i);
        }
        return sprites;
    }

    void Die()
    {
        if (audioSource != null)
            audioSource.Stop();

        Debug.Log("Level2Boss öldü");
        Destroy(gameObject);
    }
}