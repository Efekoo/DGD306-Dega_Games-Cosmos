using UnityEngine;

public class MeteorBase : MonoBehaviour
{
    public float speed = 5f;          // Hareket h�z�
    public float destroyX = -9f;      // Ekrandan ��k�nca yok olma pozisyonu
    public int health = 1;            // Dayan�kl�l�k (k���k: 1, b�y�k: 2)

    private Vector2 direction;

    void Start()
    {
        direction = Vector2.left;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            TutorialManager.Instance.OnMeteorEscaped(); // Ka�an meteor olarak say
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // Can� s�f�rland�ysa yok et
        }
    }
}