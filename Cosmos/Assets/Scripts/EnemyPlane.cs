using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPlane : MonoBehaviour
{
    public float speed = 3f;
    public int health = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    private float fireTimer;

    [Header("Yok Olma Ayarı")]
    public float destroyX = -10f; // X bu değerin altına düşerse yok olur
    
    // Zorluk ayarları
    public static float difficultyMultiplier = 1.0f;
    public static int enemiesForDifficultyIncrease = 5;
    private static int enemiesDestroyed = 0;
    public static float speedIncreaseAmount = 0.1f;
    
    
    private string currentScene;
    
    void Start()
    {
       
        currentScene = SceneManager.GetActiveScene().name;
        
        
        if (currentScene == "Level1")
        {
            difficultyMultiplier = 1.0f; // Level1'de normal hız
            enemiesDestroyed = 0;
        }
    }

    void Update()
    {
        // Level 1'de normal hız, Level 2'de artan hız kullan
        if (currentScene == "Level2")
        {
            transform.Translate(Vector2.left * speed * difficultyMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Ateş zamanlayıcı
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0f;
        }

        // Ekran dışına çıktıysa yok et
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Sadece Level 2'de zorluk artışını uygula
            if (currentScene == "Level2")
            {
                enemiesDestroyed++;
                if (enemiesDestroyed % enemiesForDifficultyIncrease == 0)
                {
                    IncreaseDifficulty();
                }
            }

            // Sadece Level1 sahnesindeyken say
            if (currentScene == "Level1")
            {
                if (Level1Manager.Instance != null)
                {
                    Level1Manager.Instance.OnEnemyDestroyed();
                }
            }

            Destroy(gameObject);
        }
    }
    
    // Zorluk seviyesini artır
    private static void IncreaseDifficulty()
    {
        difficultyMultiplier += speedIncreaseAmount;
        Debug.Log("Level 2'de düşman hızı artırıldı! Yeni çarpan: " + difficultyMultiplier);
    }
}