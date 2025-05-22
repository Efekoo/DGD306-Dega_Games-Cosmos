using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
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