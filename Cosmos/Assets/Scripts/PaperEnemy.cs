using UnityEngine;

public class PaperEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float verticalFollowSpeed = 2f;

    public float fireInterval = 1.5f;
    private float fireTimer = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    private Transform player;

    public float zigZagAmplitude = 2f;
    public float zigZagFrequency = 1f;
    private float startY;
    private float zigZagTimer = 0f;


    public int health = 3;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        startY = transform.position.y;
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
        zigZagTimer += Time.deltaTime;

        float offset = Mathf.PingPong(zigZagTimer * zigZagFrequency, zigZagAmplitude * 2) - zigZagAmplitude;

        float newY = startY + offset;

        if (player != null)
        {
            float targetY = Mathf.MoveTowards(transform.position.y, player.position.y, verticalFollowSpeed * Time.deltaTime);

            newY = (newY * 0.6f) + (targetY * 0.4f);
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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