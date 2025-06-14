using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    private string currentScene; 
    
    private float lifetime = 10f;
    private float timer = 0f;
    private int movementType;
    private float waveAmplitude = 0.5f;
    private float waveFrequency = 3f;
    private Vector3 startPosition;
    private float sinOffset;

    void Start()
    {
        
        currentScene = SceneManager.GetActiveScene().name;
        
        
        if (currentScene == "Level2")
        {
            
            movementType = Random.Range(0, 3);
            startPosition = transform.position;
            sinOffset = Random.value * Mathf.PI * 2; 
        }
    }

    void Update()
    {
       
        if (currentScene == "Level2")
        {
            
            timer += Time.deltaTime;
            
            
            if (timer > lifetime)
            {
                Destroy(gameObject);
                return;
            }
            
            
            switch (movementType)
            {
                case 0: 
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    break;
                    
                case 1: 
                    float offsetY = Mathf.Sin(Time.time * waveFrequency + sinOffset) * waveAmplitude;
                    transform.position += new Vector3(-speed * Time.deltaTime, offsetY * Time.deltaTime, 0);
                    break;
                    
                case 2: 
                    float zigzag = Mathf.PingPong(Time.time * waveFrequency, waveAmplitude * 2) - waveAmplitude;
                    transform.position += new Vector3(-speed * Time.deltaTime, zigzag * Time.deltaTime, 0);
                    break;
            }
            
          
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (screenPoint.x < -0.1f || screenPoint.x > 1.1f || 
                screenPoint.y < -0.1f || screenPoint.y > 1.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
          
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
            Destroy(gameObject);
        }
        
       
        else if (currentScene == "Level2")
        {
           
            if (other.CompareTag("Bullet"))
            {
                
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            
          
           // else if (other.CompareTag("Shield"))
           // {
               // Destroy(gameObject);
           // }
        }
    }
}