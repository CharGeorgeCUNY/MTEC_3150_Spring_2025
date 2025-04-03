using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

namespace JumpSym
{
    public class CapsuleController : MonoBehaviour
    {
        private CharacterController controller;
        public CinemachineVirtualCameraBase myCamera;
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

        private void Start()
        {
            //get player charactercontroller
            controller = gameObject.GetComponent<CharacterController>();
        }

        void Update()
        {
            //get the input, move in that direction
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 fwd = myCamera.transform.forward;
            fwd = fwd * Input.GetAxis("Vertical");
            fwd.y = 0f;
            fwd.Normalize();

            Vector3 right = myCamera.transform.right;
            right = right * Input.GetAxis("Horizontal");
            right.y = 0f;
            right.Normalize();

            playerVelocity = fwd + right * playerSpeed;
            
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

            //remember to set "Min Move Distance" to zero
            groundedPlayer = controller.isGrounded;
            playerVelocity = controller.velocity;
        }
    }
}
