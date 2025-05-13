using UnityEngine;

public class Destructable : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet2 bullet = collision.GetComponent<Bullet2>();
        if (bullet != null && !bullet.isEnemy)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);

            if (gameManager != null)
            {
                gameManager.EnemyDestroyed();
            }
        }
    }
}
