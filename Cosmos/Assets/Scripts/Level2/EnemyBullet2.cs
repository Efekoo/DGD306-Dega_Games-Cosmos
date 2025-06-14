using UnityEngine;

public class EnemyBullet2 : MonoBehaviour
{
    public float speed = 5f;
    private float lifetime = 10f;
    private float timer = 0f;

    private Vector2 moveDirection;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            float playerX = playerObj.transform.position.x;
            float fireX = transform.position.x;

            if (playerX <= fireX)
            {

                Vector2 direction = (playerObj.transform.position - transform.position).normalized;
                moveDirection = direction;
            }
            else
            {

                moveDirection = Vector2.left;
            }
        }
        else
        {

            moveDirection = Vector2.left;
        }
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

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