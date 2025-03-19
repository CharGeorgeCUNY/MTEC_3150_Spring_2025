using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
 
    public CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;
    public Camera main;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        main = Camera.main;
    }

    void Update()
    {


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 fwdComponent = new Vector3(move.x, 0.0f, move.z);
        fwdComponent.Normalize();
        Vector3 cameraForward = main.transform.forward;
        Vector3 cameraRight = main.transform.right;
        cameraForward.y = 0; //zero out the y values so that the character doesn't move up when the camera is angled downwards
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        playerVelocity = cameraForward*move.z + cameraRight*move.x;

        playerVelocity *= playerSpeed;
        playerVelocity = Vector3.ClampMagnitude(playerVelocity, playerSpeed);
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
