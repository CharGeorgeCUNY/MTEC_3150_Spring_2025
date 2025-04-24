using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicmovement : iDamageable
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float jumpForce = 5f; // Jump force if you want to add jumping later
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get the Rigidbody component attached to the player object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input for horizontal (X axis) and vertical (Z axis) movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        // Create movement vector based on player input
        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        // Apply the movement to the Rigidbody
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        // Optional: Jumping (if you want it later, based on input)
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f) // Ensure grounded before jumping
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
