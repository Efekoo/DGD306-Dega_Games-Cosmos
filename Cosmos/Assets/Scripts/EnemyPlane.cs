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
    public float destroyX = -10f;
    
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
            difficultyMultiplier = 1.0f;
            enemiesDestroyed = 0;
        }
    }

    void Update()
    {

        if (currentScene == "Level2")
        {
            transform.Translate(Vector2.left * speed * difficultyMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }


        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0f;
        }


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

            if (currentScene == "Level2")
            {
                enemiesDestroyed++;
                if (enemiesDestroyed % enemiesForDifficultyIncrease == 0)
                {
                    IncreaseDifficulty();
                }
            }


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
    

    private static void IncreaseDifficulty()
    {
        difficultyMultiplier += speedIncreaseAmount;
        Debug.Log("Level 2'de düşman hızı artırıldı yeni çarpan: " + difficultyMultiplier);
    }
}