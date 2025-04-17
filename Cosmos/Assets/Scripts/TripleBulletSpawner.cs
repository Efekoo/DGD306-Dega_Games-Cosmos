using UnityEngine;

public class TripleBulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public float angleSpread = 20f;

    void Start()
    {
        Fire();
    }

    void Fire()
    {
        for (int i = -1; i <= 1; i++)
        {
            float angle = i * angleSpread;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(bullet, transform.position, rotation);
        }

        Destroy(gameObject);
    }
}