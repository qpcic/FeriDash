using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attempts Counter")]
    int attempts = 1;
    public TMP_Text attemptText;

    
    [Header("Movement Settings")]
    public float moveSpeed = 666f;
    public float jumpVelocity = 1700f;
    public float camSpeed = 700f;

    [Header("Jump Buffer Settings")]
    public float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;

    [Header("Ground Check")]
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.6f);
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("UI")]
    public TMP_Text velocityText;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGroundedLastFrame = false;

    // For exact X movement
    private float startTime;
    private float initialX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        attempts = PlayerPrefs.GetInt("Attempts", 1);
        attemptText.text = $"Attempts: {attempts}";
        
        // Store initial time and position for X tracking
        initialX = transform.position.x;
        startTime = Time.time;
    }

    void Update()
    {
        // Handle jump buffer input
        if (Input.GetMouseButtonDown(0))
        {
            jumpBufferCounter = jumpBufferTime;
        }

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
        // Ground detection
        isGrounded = rb.IsTouchingLayers(groundLayer);

        // On landing
        if (isGrounded && !wasGroundedLastFrame)
        {
            float currentZ = transform.rotation.eulerAngles.z;
            float snappedZ = Mathf.Round(currentZ / 90f) * 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, snappedZ);
            rb.freezeRotation = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

        // On leaving ground
        if (!isGrounded && rb.freezeRotation)
        {
            rb.freezeRotation = false;
            rb.AddTorque(10f, ForceMode2D.Impulse); // optional torque when leaving ground
        }

        wasGroundedLastFrame = isGrounded;

        // Handle jump
        if (isGrounded && Mathf.Abs(rb.linearVelocity.y) < 0.01f && jumpBufferCounter > 0f)
        {
            rb.freezeRotation = false;
            rb.AddTorque(10f, ForceMode2D.Impulse);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            jumpBufferCounter = 0f;
        }

        // Override X position to enforce exact horizontal movement
        Vector3 pos = transform.position;
        pos.x = initialX + moveSpeed * (Time.time - startTime);
        transform.position = pos;

        // Move camera with player
        Camera.main.transform.position += new Vector3(camSpeed * Time.fixedDeltaTime, 0f, 0f);

        // Debug UI text
        velocityText.text =
            $"Y Velocity: {rb.linearVelocity.y:F2}\n" +
            $"X Position: {transform.position.x:F2}\n" +
            $"Grounded: {isGrounded}\n" +
            $"Rotation Z: {transform.eulerAngles.z:F2}\n" +
            $"FreezeRotation: {rb.freezeRotation}";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LevelEndWall"))
        {
            Debug.Log("YOU WON!");
            ReloadScene();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    void Die()
    {
        attempts++;
        PlayerPrefs.SetInt("Attempts", attempts); // Save to PlayerPrefs
        Debug.Log("You Died!");
        ReloadScene();
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
