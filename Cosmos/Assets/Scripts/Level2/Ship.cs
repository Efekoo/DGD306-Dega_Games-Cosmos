using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Ship : MonoBehaviour
{
    Gun[] guns;
    float moveSpeed = 3;

    bool moveUp;
    bool moveDown;
    bool moveRight;
    bool moveLeft;
    bool speedUp;
    bool shoot;

    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
    }

    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns)
            {
                if (gun != null) 
                {
                    gun.Shoot();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 3;
        }

        Vector2 move = Vector2.zero;
        if (moveUp) move.y += moveAmount;
        if (moveDown) move.y -= moveAmount;
        if (moveLeft) move.x -= moveAmount;
        if (moveRight) move.x += moveAmount;

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;

        
        pos.x = Mathf.Clamp(pos.x, 1.5f, 16f);
        pos.y = Mathf.Clamp(pos.y, 1f, 9f);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy) 
        {
            Destroy(gameObject); 
            Destroy(bullet.gameObject); 
            GoToMenu(); 
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null) 
        {
            Destroy(gameObject); 
            Destroy(destructable.gameObject);
            GoToMenu(); 
        }

        MoveSin moveSinEnemy = collision.GetComponent<MoveSin>();
        if (moveSinEnemy != null) 
        {
            Destroy(gameObject); 
            GoToMenu(); 
        }
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
