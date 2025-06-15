using UnityEngine;

public class AlienEnemyUFO : MonoBehaviour
{
    public float speed = 2f;
    public float verticalFollowSpeed = 2f;

    public float fireInterval = 0.5f;
    private float fireTimer = 0f;
    public GameObject explosionPrefab;

    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;

    private bool useFirst = true;

    private Transform player;
    
    public float zigZagAmplitude = 2f;
    public float zigZagFrequency = 1f;
    private float startY;
    private float zigZagTimer = 0f;
    public AudioClip laserSound;
    private AudioSource audioSource;


    public int health = 3;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        audioSource = GetComponent<AudioSource>();
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


        float clampedY = Mathf.Clamp(newY, -4.73f, 4.71f);

        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    void Fire()
    {

        Transform selectedFirePoint = useFirst ? firePoint1 : firePoint2;
        useFirst = !useFirst;
        
        if (audioSource != null && laserSound != null)
        {
            audioSource.PlayOneShot(laserSound);
        }

        if (bulletPrefab != null && selectedFirePoint != null)
        {
            Instantiate(bulletPrefab, selectedFirePoint.position, Quaternion.identity);
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
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}