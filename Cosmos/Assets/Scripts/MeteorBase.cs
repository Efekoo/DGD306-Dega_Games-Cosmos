using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteorBase : MonoBehaviour
{
    public float speed = 5f;
    public float destroyX = -9f;
    public int health = 1;
    public GameObject explosionPrefab;
    private bool isDead = false;

    public float rotationSpeed = 90f;

    private Vector2 direction;

    void Start()
    {
        direction = Vector2.left;
    }

    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);


        if (transform.position.x < destroyX)
        {
            if (SceneManager.GetActiveScene().name == "Tutorial" && TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnMeteorEscaped();
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log($"{gameObject.name} hasar aldý. Kalan can: {health}");

        if (health <= 0)
        {
            isDead = true;

            if (SceneManager.GetActiveScene().name == "Tutorial" && TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnMeteorDestroyed();
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}