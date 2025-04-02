using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound;  // Assign in Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = coinSound;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Walker"))
        {
            // Play sound
            audioSource.Play();

            // Hide the sprite and collider immediately
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Destroy after sound finishes playing
            Destroy(gameObject, coinSound.length);
        }
    }
}