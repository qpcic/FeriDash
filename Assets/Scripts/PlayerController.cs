using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 150f;
    public float moveSpeed = 5f; // You can tweak this in Inspector
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float jumpBufferTime = 0.15f; // in seconds

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        // If jump input is pressed or held, start buffer timer
        if (Input.GetMouseButtonDown(0))
        {
            jumpBufferCounter = jumpBufferTime;
        }

        // Optional: keep buffering while held
        if (Input.GetMouseButton(0))
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        else
        {
            jumpBufferCounter = 0f;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        
        // Auto-move to the right
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        if (isGrounded && jumpBufferCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            jumpBufferCounter = 0f; // reset buffer after jump
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LevelEndWall"))
        {
            Debug.Log("YOU WON!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
        
        // Check for spike collision
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("You Died!");
        // Simple restart for now
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}