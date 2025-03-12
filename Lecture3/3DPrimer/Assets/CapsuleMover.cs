using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMover : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        playerVelocity.x = move.x;
        playerVelocity.z = move.z;
        if (move != Vector3.zero)
        {
            Quaternion LookingAt = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookingAt, maxDegreeDelta * Time.deltaTime);
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log("Player Velocity pre Move" + playerVelocity);

        groundedPlayer = controller.isGrounded;
        playerVelocity = controller.velocity;
    }
}
