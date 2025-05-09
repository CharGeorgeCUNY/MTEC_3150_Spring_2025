    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
    [Header("Animation Settings")]
    public Animator animator;

    [Header("Rotation Settings")]
    public float rotationSpeed = 720f;
    public float smoothRotationSpeed = 5f;  // Speed at which to smoothly rotate from curl to ball

    private bool isBallForm = false;
    private bool isCurling = false;

    private Quaternion targetRotation;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isCurling)
            {
                // Start curling animation
                animator.SetTrigger("Curl");
                animator.SetBool("IsBall", false);  // Reset ball state

                isCurling = true;  // Mark as curling
            }

            // Transition to ball form if we're curling and still holding space
            if (isCurling && !isBallForm)
            {
                animator.SetBool("IsBall", true);  // Set to ball form
                isBallForm = true;  // Mark as in ball form

                // Set the target rotation for the ball form
                targetRotation = Quaternion.Euler(0f, 0f, 0f);  // Assuming ball form is at 0,0,0
            }
        }
        else
        {
            // When Spacebar is released, transition to curling and eventually idle
            if (isBallForm)
            {
                // Transition from Ball to Curling (space released)
                animator.SetTrigger("BallToCurl");  // Trigger ball-to-curl transition
                animator.SetBool("IsBall", false);  // Stop ball form
                isBallForm = false;

                // Smooth transition back to curling (reset rotation)
                targetRotation = Quaternion.Euler(0f, 0f, 0f);  // Assuming 90° rotation is the "curling" state (adjust as necessary)
            }
            else if (isCurling)
            {
                // Once curling is complete, transition to idle
                animator.SetBool("IsCurling", false);  // Stop curling animation
                animator.SetBool("IsIdle", true);  // Trigger idle animation if you have one
                isCurling = false;
            }
        }

        // Smoothly rotate between current and target rotations
        if (isBallForm)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Rotate in ball form
            Debug.Log("Rotation Speed: " + rotationSpeed); // Check the value
        }
        else if (targetRotation != transform.rotation)
        {
            // Smoothly rotate back to curling (or reset to idle)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Smooth rotation logic handled in Update
    }
}