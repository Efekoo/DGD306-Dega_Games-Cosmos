using UnityEngine;

public class Level2Boss : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject missilePrefab;
    public Transform firePoint;
    public float fireInterval = 2f;

    private float fireTimer;

    void Start()
    {
        currentHealth = maxHealth;
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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Level2Boss öldü");
        Destroy(gameObject);

    }
}