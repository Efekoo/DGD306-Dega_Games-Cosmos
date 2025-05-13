using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    Vector2 direction;

    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    void Start()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    void Update()
    {
        if (autoShoot && IsVisible()) 
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }

    bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    public void Shoot()
    {
        if (this == null) return;  

        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();

        if (goBullet != null)
        {
            goBullet.direction = direction;
        }
    }
}