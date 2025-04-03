using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpSym
{
    public class CapsuleController_FPS : MonoBehaviour
    {
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        
        [SerializeField]
        private float playerSpeed = 2.0f;

        [SerializeField]
        private float jumpHeight = 1.0f;
        
        [SerializeField]
        private float gravityValue = -9.81f;

        [SerializeField]
        private float maxDegreeDelta = 45.0f;

        [SerializeField]
        private float turnSpeed = 45.0f;

        [SerializeField]
        private GameObject myCamera;

        private float CurrentXRotation = 0;

        private void Start()
        {
            //get player charactercontroller
            controller = gameObject.GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            //get the input, move in that direction
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            playerVelocity.x = move.x;
            playerVelocity.y = move.y;

            // Makes the player jump
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            //remember to set "Min Move Distance" to zero
            groundedPlayer = controller.isGrounded;
            playerVelocity = controller.velocity;

            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime);
            CurrentXRotation += Input.GetAxis("Mouse Y") * -1 * turnSpeed * Time.deltaTime;

            //prevent camera from rotating too far
            CurrentXRotation = Mathf.Clamp(CurrentXRotation, -75f, 75f);

            myCamera.transform.localRotation = Quaternion.Euler(CurrentXRotation, myCamera.transform.rotation.y, myCamera.transform.rotation.z);
        }
    }
}
