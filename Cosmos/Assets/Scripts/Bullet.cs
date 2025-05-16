using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteor"))
        {
            MeteorBase meteor = other.GetComponent<MeteorBase>();

            if (meteor != null)
            {
                meteor.TakeDamage(1);
                TutorialManager.Instance.OnMeteorDestroyed();
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            EnemyPlane enemy = other.GetComponent<EnemyPlane>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}