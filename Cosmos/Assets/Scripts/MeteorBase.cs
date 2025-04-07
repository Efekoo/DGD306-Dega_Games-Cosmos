using UnityEngine;

public class MeteorBase : MonoBehaviour
{
    public float speed = 5f;
    public float destroyX = -4f;

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
            Destroy(gameObject);
        }
    }
}