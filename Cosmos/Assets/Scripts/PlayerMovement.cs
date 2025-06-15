using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
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
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 5);

        Debug.Log("Can: " + health);
        UpdateHealthUI();

        if (health <= 0)
        {
            string scene = SceneManager.GetActiveScene().name;

            if (scene == "Tutorial")
                TutorialManager.Instance.OnPlayerDied();
            else if (scene == "Level1")
                Level1Manager.Instance.OnPlayerDied();
            else if (scene == "Level2")
                Level2Manager.Instance.OnPlayerDied();
            else if (scene == "Level2Boss")
                Level2BossManager.Instance.OnPlayerDied();

                Destroy(gameObject);
        }
    }
    public void Heal(int amount)
    {
        if (health < 5)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, 5);
            UpdateHealthUI();
            Debug.Log("Can alındı! Yeni can: " + health);
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