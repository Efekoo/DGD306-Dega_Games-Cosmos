using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isPlayerOne = true;

    [Header("Hareket Sınırları")]
    public float minX = -8.31f;
    public float maxX = 8.34f;
    public float minY = -4.73f;
    public float maxY = 4.71f;

    [Header("Can Sistemi")]
    public int health = 5;
    public Image healthBarImage;
    public Sprite[] healthSprites;

    void Start()
    {
        UpdateHealthUI();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (isPlayerOne)
        {
            if (Input.GetKey(KeyCode.A)) horizontal = -1f;
            if (Input.GetKey(KeyCode.D)) horizontal = 1f;
            if (Input.GetKey(KeyCode.W)) vertical = 1f;
            if (Input.GetKey(KeyCode.S)) vertical = -1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;
            if (Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) vertical = -1f;
        }

        Vector3 move = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 5);

        Debug.Log((isPlayerOne ? "P1" : "P2") + " canı: " + health);
        UpdateHealthUI();

        if (health <= 0)
        {
            Debug.Log((isPlayerOne ? "P1" : "P2") + " öldü!");

            
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                TutorialManager.Instance.OnPlayerDied();
            }
            else if (SceneManager.GetActiveScene().name == "Level1")
            {
                Level1Manager.Instance.OnPlayerDied();
            }

            Destroy(gameObject);
        }
    }


    void UpdateHealthUI()
    {
        if (healthBarImage != null && healthSprites != null && health >= 0 && health <= 5)
        {
            healthBarImage.sprite = healthSprites[health];
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SmallMeteor"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BigMeteor"))
        {
            TakeDamage(2);
            Destroy(other.gameObject);
        }
    }
}