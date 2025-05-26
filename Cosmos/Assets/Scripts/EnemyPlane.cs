using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float speed = 3f;
    public int health = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    private float fireTimer;

    [Header("Yok Olma Ayarý")]
    public float destroyX = -10f; // X bu deðerin altýna düþerse yok olur

    void Update()
    {
        // Hareket
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Ateþ zamanlayýcý
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0f;
        }

        // Ekran dýþýna çýktýysa yok et
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
            // Sadece Level1 sahnesindeyken say
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level1")
            {
                if (Level1Manager.Instance != null)
                {
                    Level1Manager.Instance.OnEnemyDestroyed();
                    Debug.Log("Enemy destroyed! Total: " + Level1Manager.Instance); // test için
                }
            }

            Destroy(gameObject);
        }
    }
}