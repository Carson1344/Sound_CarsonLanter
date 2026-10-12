using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public float floatForce = 15f; // Added a default value so it's not 0
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public InputAction floatAction;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    void Start()
    {
        // 1. FIXED: Get and assign the Rigidbody component
        playerRb = GetComponent<Rigidbody>();

        // 2. FIXED: Assigning global gravity in Start() multiplies it EVERY restart.
        // It's safer to adjust the Rigidbody's mass or use a custom gravity script,
        // but if keeping this, make sure it fits your project workflow.
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);

        playerAudio = GetComponent<AudioSource>();
        floatAction.Enable();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // 3. FIXED: Continuous physics forces should always happen in FixedUpdate
    void FixedUpdate()
    {
        // While space is pressed and player is low enough, float up
        if (floatAction.IsPressed() && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }

    // Clean up input when the object is destroyed or disabled
    private void OnDisable()
    {
        floatAction.Disable();
    }

    private void OnCollisionEnter(Collision other)
    {
        // If player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
        // If player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}