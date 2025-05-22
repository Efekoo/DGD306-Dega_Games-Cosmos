using UnityEngine;
using TMPro;

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
    public int health = 3;
    public TMP_Text healthText;

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
        Debug.Log((isPlayerOne ? "P1" : "P2") + " canı: " + health);
        UpdateHealthUI();

        if (health <= 0)
        {
            Debug.Log((isPlayerOne ? "P1" : "P2") + " öldü!");
            Destroy(gameObject);
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            string label = isPlayerOne ? "P1 Health: " : "P2 Health: ";
            healthText.text = label + health.ToString();
        }
    }
}