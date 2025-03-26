using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CapsuleAquaMove : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 5.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public Camera main;
    

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        main = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 fwd = main.transform.forward;
        fwd.y = 0.0f;
        fwd.Normalize();

        Vector3 rght = main.transform.right;
        rght.y = 0.0f;
        rght.Normalize();

        fwd = fwd * Input.GetAxis("Vertical");
        
        rght = rght * Input.GetAxis("Horizontal");
        //same the y and reapply after playerVelocity is reassigned to the player's updated movement
        float y = playerVelocity.y;
        playerVelocity = (fwd + rght) * playerSpeed;
        playerVelocity.y = y;

        if (move != Vector3.zero)
        {
            Quaternion LookingAt = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookingAt, maxDegreeDelta * Time.deltaTime);
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            //calculus - jump height times -2 times -9.81 (gravity) sqrted
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        groundedPlayer = controller.isGrounded;
        playerVelocity = controller.velocity;
    }
}