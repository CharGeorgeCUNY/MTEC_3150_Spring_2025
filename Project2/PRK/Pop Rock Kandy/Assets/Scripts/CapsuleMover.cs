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
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public Camera main;
    

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        main = Camera.main;
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
        playerVelocity = (fwd + rght) * playerSpeed;
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

        if(Input.GetKeyDown(KeyCode.X))
        {
            CameraOne.GetComponent<CinemachineVirtualCameraBase>().Priority = 50;

            CameraTwo.GetComponent<CinemachineVirtualCameraBase>().Priority = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraOne.GetComponent<CinemachineVirtualCameraBase>().Priority = 0;

            CameraTwo.GetComponent<CinemachineVirtualCameraBase>().Priority = 50;
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        groundedPlayer = controller.isGrounded;
        playerVelocity = controller.velocity;
    }
}