using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMove_FPS : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;
    public float TurnSpeed = 45.0f;
    public GameObject CameraRig;
    private float CurrentXRotation = 0;
    private float TargetZRotation = 0;
    private bool IsFlipped = false;

    private float CameraFlip = 0.0f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            TargetZRotation += 180.0f;
            IsFlipped = !IsFlipped;

        }

        CameraFlip = Mathf.MoveTowards(CameraFlip, TargetZRotation, 360.0f * Time.deltaTime);
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(!IsFlipped)
        {
            playerVelocity = (transform.forward * move.z + transform.right * move.x) * playerSpeed;
        }
        else
        {
            playerVelocity = (transform.forward * move.z + transform.right * -move.x) * playerSpeed;
        }

        

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        groundedPlayer = controller.isGrounded;
        playerVelocity = controller.velocity;

        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * TurnSpeed * Time.deltaTime );
        if (!IsFlipped)
        {
            CurrentXRotation += Input.GetAxis("Mouse Y") * TurnSpeed * Time.deltaTime;
        }
        else {
            CurrentXRotation -= Input.GetAxis("Mouse Y") * TurnSpeed * Time.deltaTime;
        }

        CurrentXRotation = Mathf.Clamp(CurrentXRotation, -90f, 90f);

        
      

        CameraRig.transform.localRotation = Quaternion.Euler(CurrentXRotation, CameraRig.transform.localRotation.y, CameraRig.transform.localRotation.z + CameraFlip);

    }
}
