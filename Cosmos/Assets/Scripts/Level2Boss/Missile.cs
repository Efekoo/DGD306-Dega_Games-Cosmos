using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 1f;
    private Transform currentTarget;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        FindClosestTarget();

        if (currentTarget == null) return;


        Vector2 direction = (currentTarget.position - transform.position).normalized;


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);


        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void FindClosestTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = player.transform;
            }
        }

        currentTarget = closest;
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