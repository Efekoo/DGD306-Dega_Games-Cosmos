using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public Transform firePoint;

    public bool isTripleShot = false;
    public float fireRate = 0.5f;
    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && fireTimer >= fireRate)
        {
            if (isTripleShot)
            {
                Instantiate(tripleBulletPrefab, firePoint.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            }

            fireTimer = 0f;
        }
    }
}