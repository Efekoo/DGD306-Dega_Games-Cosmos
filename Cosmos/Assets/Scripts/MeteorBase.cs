using UnityEngine;

public class MeteorBase : MonoBehaviour
{
    public float speed = 5f;          // Hareket hýzý
    public float destroyX = -9f;      // Ekrandan çýkýnca yok olma pozisyonu
    public int health = 1;            // Dayanýklýlýk (küçük: 1, büyük: 2)

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
            Destroy(gameObject); // Ekrandan çýkarsa yok et
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // Caný sýfýrlandýysa yok et
        }
    }
}