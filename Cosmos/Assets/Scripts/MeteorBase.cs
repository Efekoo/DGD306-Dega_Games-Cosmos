using UnityEngine;

public class MeteorBase : MonoBehaviour
{
    public float speed = 5f;
    public float destroyX = -9f;
    public int health = 1;

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
            TutorialManager.Instance.OnMeteorEscaped();
            Destroy(gameObject);
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