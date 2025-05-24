using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPlatformerMovement : MonoBehaviour
{
    public bool isPlayerOne = true;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Zemin Tespiti")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = 0f;
        bool jumpInput = false;

        if (isPlayerOne)
        {
            if (Input.GetKey(KeyCode.A)) moveInput = -1f;
            if (Input.GetKey(KeyCode.D)) moveInput = 1f;
            if (Input.GetKeyDown(KeyCode.W)) jumpInput = true;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveInput = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveInput = 1f;
            if (Input.GetKeyDown(KeyCode.UpArrow)) jumpInput = true;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpInput && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}