using UnityEngine;

public class PaperEnemy : MonoBehaviour
{
    public float speed = 4f;
    public float verticalFollowSpeed = 2f;

    public float fireInterval = 2f;
    private float fireTimer = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    private Transform player;

    


    public int health = 2;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

        ZigZagMovement();

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0f;
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    void ZigZagMovement()
    {
        if (player != null)
        {
            float currentY = transform.position.y;
            float targetY = Mathf.MoveTowards(currentY, player.position.y, verticalFollowSpeed * Time.deltaTime);
            float clampedY = Mathf.Clamp(targetY, -4.73f, 4.71f);


            float deltaY = clampedY - currentY;
            float angle = Mathf.Clamp(deltaY * 30f, -30f, 30f);


            transform.rotation = Quaternion.Euler(0f, 0f, -angle);

            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
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
            if (Level2Manager.Instance != null)
            {
                Level2Manager.Instance.OnEnemyDestroyed();
            }

            Destroy(gameObject);
        }
    }


}