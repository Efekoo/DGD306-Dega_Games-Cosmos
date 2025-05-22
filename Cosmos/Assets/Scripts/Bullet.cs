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
        // Küçük meteor
        if (other.CompareTag("SmallMeteor"))
        {
            MeteorBase meteor = other.GetComponent<MeteorBase>();
            if (meteor != null)
            {
                meteor.TakeDamage(1);
                TutorialManager.Instance.OnMeteorDestroyed();
            }

            Destroy(gameObject);
        }

        // Büyük meteor
        else if (other.CompareTag("BigMeteor"))
        {
            MeteorBase meteor = other.GetComponent<MeteorBase>();
            if (meteor != null)
            {
                meteor.TakeDamage(1);
                TutorialManager.Instance.OnMeteorDestroyed();
            }

            Destroy(gameObject);
        }

        else if (other.CompareTag("Enemy"))
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