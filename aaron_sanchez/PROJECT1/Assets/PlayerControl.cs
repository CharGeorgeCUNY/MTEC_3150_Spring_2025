using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


using TMPro;  //TextMeshPro 

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;          // Player movement speed
    public int lives = 3;             // Player's lives
    public int score = 0;             // Player's score

    // Reference to UI TextMeshPro components
    public TextMeshProUGUI scoreText; // Score Text UI

    private Rigidbody2D rb;       

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        StartCoroutine(IncrementScore());  //  increase score every 5 seconds (I used chatgpt for this)
    }

    void Update()
    {
        
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    // Function to handle the  damage
    public void TakeDamage()
    {
        lives--;  // Decrease lives
        if (lives <= 0)
        {
            // When lives are 0 or less, Game Over
            scoreText.text = "Game Over!";
            Destroy(gameObject);  // Destroy player object
        }
    }

    // add score
    public void AddScore()
    {
        score += 100;  // Add 100 to score
        scoreText.text = "Score: " + score;  // Update the UI with new score

        // Check if the score has reached 100 to win
        if (score >= 100)
        {
            scoreText.text = "You Win!";  
            Time.timeScale = 0;  // Stops my game
        }
    }

    
    private IEnumerator IncrementScore()
    {
        while (true)  // This will keep running forever (until I stop it)
        {
            yield return new WaitForSeconds(5f);  // Wait for 5 seconds
            score += 10;  // Increase score by 10
            scoreText.text = "Score: " + score; 

            // Check if score reached 100 to win
            if (score >= 100)
            {
                scoreText.text = "You Win!";
                Time.timeScale = 0;  // Stop the game
                yield break;  // Stop the Coroutine once the game is won(Used chatgpt for yields )
            }
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("fallingObject"))


        {
            TakeDamage();  // 
        }
    }
}