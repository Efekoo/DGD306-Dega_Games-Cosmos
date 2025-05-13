using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public Vector2 velocity;
    public bool isEnemy = false;

    bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }

    void Update()
    {
        velocity = direction * speed;

        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsVisible())
            return;

        Bullet2 bullet = collision.GetComponent<Bullet2>();
        if (bullet != null && bullet.isEnemy != isEnemy)
        {
            Destructable destructable = collision.GetComponent<Destructable>();
            if (destructable != null && !bullet.isEnemy)
            {
                Destroy(destructable.gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }
}
