using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    private float lifetime = 10f;
    private float timer = 0f;

    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);


        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
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
    }
}