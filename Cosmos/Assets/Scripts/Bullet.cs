using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Artýk saða doðru gidiyor
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteor"))
        {
            MeteorBase meteor = other.GetComponent<MeteorBase>();

            if (meteor != null)
            {
                meteor.TakeDamage(1); // Hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}