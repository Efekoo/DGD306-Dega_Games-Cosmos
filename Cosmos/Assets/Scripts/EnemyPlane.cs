using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float speed = 3f;
    public int health = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    private float fireTimer;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0f;
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
            Destroy(gameObject);
        }
    }
}
