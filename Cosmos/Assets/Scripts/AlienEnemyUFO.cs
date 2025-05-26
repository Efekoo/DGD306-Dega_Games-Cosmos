using UnityEngine;

public class AlienEnemyUFO : MonoBehaviour
{
    public float speed = 2f;
    public float verticalFollowSpeed = 2f;

    public float fireInterval = 1.5f;
    private float fireTimer = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;

    private bool useFirst = true;

    private Transform player;

    void Start()
    {
        // Player'ı sahnede bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // X ekseninde sola hareket
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

        // Eğer player varsa Y ekseninde ona yaklaş
        if (player != null)
        {
            Vector3 pos = transform.position;
            float step = verticalFollowSpeed * Time.deltaTime;
            pos.y = Mathf.MoveTowards(pos.y, player.position.y, step);
            transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
        }

        // Ateş zamanı geldi mi?
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0f;
        }

        // Ekran dışına çıkarsa yok et
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        Transform selectedFirePoint = useFirst ? firePoint1 : firePoint2;
        useFirst = !useFirst;

        if (bulletPrefab != null && selectedFirePoint != null)
        {
            Instantiate(bulletPrefab, selectedFirePoint.position, Quaternion.identity);
        }
    }
}