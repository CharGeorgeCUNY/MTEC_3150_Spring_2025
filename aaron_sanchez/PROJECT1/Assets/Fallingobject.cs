using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class fallingObject : MonoBehaviour
{
    public float fallSpeed = 3f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
                player.AddScore();
            }
            Destroy(gameObject);
        }
    }
}

        
    


