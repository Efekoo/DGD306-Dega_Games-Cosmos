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
    
    // ZigZag hareketi için gerekli değişkenler
    public float zigZagAmplitude = 2f;  // ZigZag genliği
    public float zigZagFrequency = 1f;  // ZigZag hızı
    private float startY;
    private float zigZagTimer = 0f;

    void Start()
    {
        // Player'ı sahnede bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        // Başlangıç pozisyonunu kaydet
        startY = transform.position.y;
    }

    void Update()
    {
        // X ekseninde sola hareket
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

        // ZigZag hareketi
        ZigZagMovement();

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

    void ZigZagMovement()
    {
        zigZagTimer += Time.deltaTime;
        
        // Ping-pong fonksiyonu 0 ile zigZagAmplitude*2 arasında gidip gelir
        float offset = Mathf.PingPong(zigZagTimer * zigZagFrequency, zigZagAmplitude * 2) - zigZagAmplitude;
        
        // Y pozisyonunu başlangıç pozisyonuna göre güncelle
        float newY = startY + offset;
        
        // Eğer oyuncu varsa, onun Y pozisyonuna doğru da hareket et
        if (player != null)
        {
            // Oyuncuya doğru kademeli hareketi hesapla
            float targetY = Mathf.MoveTowards(transform.position.y, player.position.y, verticalFollowSpeed * Time.deltaTime);
            
            // Zig-zag ve oyuncuya takip hareketlerini birleştir (%60 zigzag, %40 oyuncu takibi)
            newY = (newY * 0.6f) + (targetY * 0.4f);
        }
        
        // Yeni pozisyonu uygula
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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