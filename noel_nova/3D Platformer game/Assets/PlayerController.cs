using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from WASD keys
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f; // Move forward
        if (Input.GetKey(KeyCode.S)) moveZ = -1f; // Move backward
        if (Input.GetKey(KeyCode.A)) moveX = -1f; // Move left
        if (Input.GetKey(KeyCode.D)) moveX = 1f; // Move right

        // Normalize movement to avoid faster diagonal movement
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // Jumping with Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 300);
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is on the ground or platforms
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}