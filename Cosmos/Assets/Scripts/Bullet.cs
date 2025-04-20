using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Art�k sa�a do�ru gidiyor
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

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}