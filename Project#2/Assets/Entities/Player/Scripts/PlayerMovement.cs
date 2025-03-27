using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        float playerHeight = GetComponent<Collider>().bounds.size.y;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        grounded = Physics.Raycast(rayOrigin, rayDirection, playerHeight * 0.5f + 0.3f, whatIsGround);
        Debug.DrawRay(rayOrigin, rayDirection * (playerHeight * 0.5f + 0.3f), grounded ? Color.green : Color.red);

        MyInput();
        SpeedControl();

        // Handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }


    private void FixedUpdate()
    {
        if (!grounded) { rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration); }
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on the ground: apply movement
        if (grounded)
        {
            Vector3 targetVelocity = moveDirection.normalized * moveSpeed;
            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        }
        else
        {
            Vector3 targetVelocity = moveDirection.normalized * moveSpeed * airMultiplier;
            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}