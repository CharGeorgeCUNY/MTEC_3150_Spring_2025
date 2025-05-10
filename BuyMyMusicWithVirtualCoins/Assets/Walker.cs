using UnityEngine;
using TMPro;

public class Walker : MonoBehaviour
{
    public float speed = 1f;
    //public float speedIncreasePerCoin = 0.03f;
    public Vector2 velocity;
    public Rigidbody2D rigidBody2D;
    bool IsWalking;
    public Animator animator;
    private int coinCount = 0; // Track collected coins
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to UI Text
    private bool canPlayMusic = false; // Only allow music trigger when 10 coins are collected

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Find ScoreText in Scene
        if (scoreText == null)
        {
            GameObject textObj = GameObject.Find("ScoreText");
            if (textObj != null && textObj.TryGetComponent(out TextMeshProUGUI tmp))
            {
                scoreText = tmp;
            }
        }

        UpdateScoreUI();
    }

    void Update()
    {
        velocity = Vector2.zero;
        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float NormalizedSpeed = velocity.magnitude;

        IsWalking = NormalizedSpeed > 0.2f;
        Debug.Log("Speed" + NormalizedSpeed);
        velocity.Normalize();
        animator.SetFloat("Horizontal", velocity.x);
        animator.SetFloat("Vertical", velocity.y);
        animator.SetBool("IsWalking", IsWalking);

        velocity *= speed;
        rigidBody2D.velocity = velocity;
    }

    // Detect coin collection and update score
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            //speed += speedIncreasePerCoin; // Increase speed per coin collected
            UpdateScoreUI();
            Debug.Log("Coins Collected: " + coinCount + " | Speed: " + speed);
            Destroy(other.gameObject);

            // Allow music trigger when 10 coins are collected
            if (coinCount >= 99)
            {
                canPlayMusic = true;
                Debug.Log("99 Coins Collected! Find PlayMusic trigger.");
            }
        }

        // Check if Walker touches "PlayMusic" object after collecting 10 coins
        if (canPlayMusic && other.gameObject.CompareTag("PlayMusic"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play(); // Play the MusicX track
                Debug.Log("Playing MusicX!");
            }
        }
    }

    // Update UI text for the score
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.LogWarning("ScoreText UI is not assigned or found in the scene.");
        }
    }
}