using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    private bool isDead = false;

    //[Header("Effects")]
    //public GameObject deathExplosionPrefab;

    [Header("Attempts Counter")]
    int attempts = 1;

    [Header("Audio")]
    public AudioSource deathAudioSource;

    private int totalAttempts;
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

    private float startTime;
    private float initialX;

    public LevelProgress levelProgress;

    private bool isPaused = false;
    private int jumpCount = 0;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        totalAttempts = PlayerPrefs.GetInt("TotalAttempts");

        attempts = PlayerPrefs.GetInt("Attempts", 1);
        attemptText.text = $"Attempts: {attempts}";

        initialX = transform.position.x;
        Time.timeScale = 1f;
        startTime = Time.time;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            jumpBufferCounter = jumpBufferTime;
            jumpCount++;    
        }

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) 
            jumpBufferCounter -= Time.deltaTime;
        else
            jumpBufferCounter = 0f;

        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    void FixedUpdate() {
        if (isPaused || isDead) return;

        isGrounded = rb.IsTouchingLayers(groundLayer);

        if (isGrounded && !wasGroundedLastFrame) {
            float currentZ = transform.rotation.eulerAngles.z;
            float snappedZ = Mathf.Round(currentZ / 90f) * 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, snappedZ);
            rb.freezeRotation = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

        if (!isGrounded && rb.freezeRotation) {
            rb.freezeRotation = false;
            rb.AddTorque(10f, ForceMode2D.Impulse);
        }

        wasGroundedLastFrame = isGrounded;

        if (isGrounded && Mathf.Abs(rb.linearVelocity.y) < 0.01f && jumpBufferCounter > 0f) {
            rb.freezeRotation = false;
            rb.AddTorque(10f, ForceMode2D.Impulse);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            jumpBufferCounter = 0f;
        }

        Vector3 pos = transform.position;
        pos.x = initialX + moveSpeed * (Time.time - startTime);
        transform.position = pos;

        Camera.main.transform.position += new Vector3(camSpeed * Time.fixedDeltaTime, 0f, 0f);

        velocityText.text =
            $"Y Velocity: {rb.linearVelocity.y:F2}\n" +
            $"X Position: {transform.position.x:F2}\n" +
            $"Grounded: {isGrounded}\n" +
            $"Rotation Z: {transform.eulerAngles.z:F2}\n" +
            $"FreezeRotation: {rb.freezeRotation}";
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("LevelEndWall")) {
            levelProgress.CompleteLevel();
            Debug.Log("YOU WON!");
            TogglePause();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Spike"))
            Die();
    }

    void Die() {
        if (deathAudioSource != null)
            deathAudioSource.Play();

        isDead = true;

        // Zakomentiran efekt smrti:
        /*
        if (deathExplosionPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.z = 0; // ali -1, če kamera ni na istem Z
            GameObject fx = Instantiate(deathExplosionPrefab, spawnPosition, Quaternion.identity);
            fx.transform.localScale = Vector3.one; // če si imel skalirano na 20, bo zdaj normalno
        }
        */

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Static;

        GetComponent<Collider2D>().enabled = false;

        attempts++;
        PlayerPrefs.SetInt("Attempts", attempts);
        Debug.Log("You Died!");

        int totAttempts = PlayerPrefs.GetInt("TotalAttempts", 0);
        PlayerPrefs.SetInt("TotalAttempts", totAttempts + 1);

        int jumps = PlayerPrefs.GetInt("TotalJumps", 0);
        PlayerPrefs.SetInt("TotalJumps", jumps + jumpCount);

        int isAutoRetry = PlayerPrefs.GetInt("AutoRetry", 1);
        if (isAutoRetry == 1) {
            Invoke(nameof(ReloadScene), 0.35f);
        } else {
            TogglePause();
        }
    }

    public void ReloadScene() {
        if (isPaused) {
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync("LevelPause");
            isPaused = false;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePause() {
        if (!isPaused) {
            Time.timeScale = 0f;
            isPaused = true;
            PlayerPrefs.SetFloat("Time", Time.time - startTime);
            PlayerPrefs.SetInt("Jumps", jumpCount);
            SceneManager.LoadScene("LevelPause", LoadSceneMode.Additive);
        } else {
            isPaused = false;
            SceneManager.UnloadSceneAsync("LevelPause");
        }
    }

    public void OnRestartButtonClicked() {
        ReloadScene();
    }

    public void OnMainMenuButtonClicked() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
