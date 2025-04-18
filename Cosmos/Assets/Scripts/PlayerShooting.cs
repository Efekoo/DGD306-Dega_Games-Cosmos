using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public Transform firePoint;

    public bool isPlayerOne = true;
    public bool isTripleShot = false;
    public float fireRate = 0.5f;
    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (isPlayerOne && Input.GetKeyDown(KeyCode.Space) && fireTimer >= fireRate)
        {
            Fire();
        }
        else if (!isPlayerOne && Input.GetKeyDown(KeyCode.RightShift) && fireTimer >= fireRate)
        {
            Fire();
        }
    }
    void Fire()
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