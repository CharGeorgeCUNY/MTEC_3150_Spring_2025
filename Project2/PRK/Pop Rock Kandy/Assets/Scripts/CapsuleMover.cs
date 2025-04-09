using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CapsuleMover : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;
    // public GameObject CameraOne;
    // public GameObject CameraTwo;
    public Camera mainCam;
    
    bool isWalking;
    bool isJumping;
    public Animator animator;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        Vector3 forward = mainCam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 rght = mainCam.transform.right;
        rght.y = 0.0f;
        rght.Normalize();

        forward *= Input.GetAxis("Vertical");
        
        rght *= Input.GetAxis("Horizontal");

        // Save the y and reapply after playerVelocity is reassigned to the player's updated movement
        float y = playerVelocity.y;

        playerVelocity = (forward + rght) * playerSpeed;
        
        if (move != Vector3.zero)
        {
            Quaternion LookingAt = Quaternion.LookRotation(playerVelocity, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookingAt, maxDegreeDelta * Time.deltaTime);
            // transform.rotation = LookingAt;
            
        }
        
        playerVelocity.y = y;

        playerVelocity.Normalize();
        animator.SetBool("isWalking", isWalking);

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            // Calculus | 1-5 (jump height) times -2 times -9.81 (gravity) square rooted

            
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        groundedPlayer = controller.isGrounded;
        playerVelocity = controller.velocity;

        float NormalizedSpeed = playerVelocity.magnitude;

        if (NormalizedSpeed > .2f)
        {
            isWalking = true;
        }

        else
        {
            isWalking = false;
        }

        if (NormalizedSpeed > .2f)
        {
            isJumping = true;
        }

        else
        {
            isJumping = false;
        }

        playerVelocity.Normalize();
        animator.SetBool("isJumping", isJumping);
    }
}