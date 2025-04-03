using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using UnityEngine;

namespace JumpSym.Player
{
    public class Locomotion : MonoBehaviour
    {
        [Header("Camera Control")]
        public float mouseSensitivity = 3.5f;
        public float cameraPitch { get; private set; } = 0.0f;

        [Header("Movement Control")]
        public float walkSpeed = 6.0f;
        public float runSpeed = 9.0f;
        private float climbSpeed = 0.0f;

        public float currentSpeed = 0.0f;


        [Range(0.0f, 0.5f)] public float smoothing = 0.1f;

        [Range(0.0f, 0.5f)] public float mouseSmoothing = 0.03f;

        [Header("Movement Vectors")]
        private Vector2 currentDir = Vector2.zero;
        private Vector2 currentDir_Velocity = Vector2.zero;

        private Vector2 targetDir = Vector2.zero;

        private Vector2 currentMouseDelta = Vector2.zero;
        private Vector2 currentMouseDelta_Velocity = Vector2.zero;

        public Vector3 verticalVelocity = Vector3.zero;

        public Vector3 velocity = Vector3.zero;

        [Header("Physics")]
        public float jumpHeight = 5.0f;
        public float gravity = -9.81f;
        public float terminalVelocity = -100f;
        [Range(0, 1)] public float airDrag = 0.1f;


        [Header("Collision")]
        [SerializeField] private float collisionYOffset;
        [SerializeField] private Transform headBone;
        [SerializeField] private Transform footBone_L;
        [SerializeField] private Transform footBone_R;


        [Header("State Bools")]
        public bool isGrounded = true;

        public bool isWalking;
        public bool isRunning;
        public bool isJumping;
        public bool isFalling;

        public bool isClimbing;


        [Header("Animation Data")]
        public float movementX;
        public float movementY;
        public float movementZ;
     
        
        public void Move()
        {
            //only move if player is in control and not climbing
            if (!Controller.interactions.inControl || isClimbing) return;

            //am i walking or running?            
            if (isWalking)
            {
                if (isRunning)
                {
                    currentSpeed = runSpeed;
                }

                else
                {
                    currentSpeed = walkSpeed;
                }
            }

            //if jumping or falling, decrease momentum slightly
            if (isJumping || isFalling)
            {
                targetDir.x *= airDrag;
                targetDir.y *= airDrag;
            }

            //store input x, y in a Vector2, only if not jumping or falling
            if (!isJumping && !isFalling)
            {
                targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }

            //prevent diagonal movement from being faster
            targetDir.Normalize();

            movementX = targetDir.x;
            movementY = targetDir.y;

            //smooth from current location to target location over smoothTime
            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDir_Velocity, smoothing);

            //first, check if we are grounded using ScanFloor(), but only if we aren't jumping or in an action
            if (!isJumping && !Controller.interactions.inAction)
            {

                //find if grounded using cc isGrounded OR spherecast
                if (CheckGrounded())
                {
                    //reset variables if we are grounded
                    isGrounded = true;
                    isJumping = false;
                    isFalling = false;
                }

                else
                {
                    isGrounded = false;
                }

            }

            //next, if we are grounded, check if the player has tried to jump
            if (isGrounded && !isJumping && Input.GetButtonDown("Jump"))
            {
                //trigger jumping animation and switch to state isJumping
                Controller.animator.SetTrigger("Jumping");
                isJumping = true;
                isGrounded = false;
                //also make sure you actually jump up
                verticalVelocity = Vector3.up * jumpHeight;
            }

            //check if we have started falling and switch to state isFalling
            if (Controller.animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                isJumping = false;
                isFalling = true;
            }

            //check if we're currently falling without having jumped first
            if (verticalVelocity.y < gravity - 2)
            {
                isFalling = true;
            }

            isGrounded = CheckGrounded();

            //add gravity only if we are falling
            if (!isJumping && !isGrounded)
            {
                //Vector3(0, gravity * time, 0)
                verticalVelocity += Vector3.up * gravity * Time.deltaTime;

                //make sure vV doesnt exceed terminal velocity
                verticalVelocity.y = Mathf.Max(terminalVelocity, verticalVelocity.y);
            }

            isGrounded = CheckGrounded();

            //remove gravity if we have landed and set isFalling to false
            if (!isJumping && isGrounded)
            {
                isFalling = false;
                verticalVelocity.y = -9.81f;
            }

            //multiply movement by speed
            //velocity = ((movement forward/backward) + (movement left/right) * movement speed) + movement up/down
            velocity = (((transform.forward * currentDir.y) + (transform.right * currentDir.x)) * currentSpeed) + verticalVelocity;

            //move the playeryVelocity_Max
            Controller.characterController.Move(velocity * Time.deltaTime);

            //check if we're idle, walking, or running            
            if (movementX != 0 || movementY != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    isWalking = true;
                    isRunning = true;
                }

                else
                {
                    isWalking = true;
                    isRunning = false;
                }
            }

            else

            {
                isWalking = false;
                isRunning = false;
            }
        }

        public void MoveCamera()
        {
            if (Controller.interactions.cameraControl)
            {
                //get the location (x, y) of the mouse
                Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDelta_Velocity, mouseSmoothing);

                //rotate on the x axis up/down, within range (-75, 75)
                cameraPitch -= currentMouseDelta.y * mouseSensitivity;
                cameraPitch = Mathf.Clamp(cameraPitch, -75, 75);


                Controller.playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;


                //rotate on the y axis left/right

                transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

            }
        }

        // public void MoveClimbing()
        // {
        //     //only climb if we're climbing
        //     if (!isClimbing) return;

        //     isWalking = false;
        //     currentSpeed = climbSpeed;

        //     movementX = 0f;
        //     movementY = 0f;
        //     movementZ = Input.GetAxisRaw("Vertical");

        //     Controller.animator.speed = Mathf.Abs(movementZ);
        // }


        public void PlayAnimations()
        {
            Controller.animator.SetFloat("MovingX", movementX);
            Controller.animator.SetFloat("MovingY", movementY);
            // Controller.animator.SetFloat("MovingZ", movementZ);

            Controller.animator.SetBool("IsWalking", isWalking);
            Controller.animator.SetBool("IsRunning", isRunning);
            // Controller.animator.SetBool("IsClimbing", isClimbing);

            Controller.animator.SetBool("IsFalling", isFalling);
            Controller.animator.SetBool("IsGrounded", isGrounded);

        }

        public bool CheckGrounded()
        {
            var scanData = Controller.worldScanner.ScanFloor();
            if (Controller.characterController.isGrounded || scanData.HitFoundV == true) return true;
            else return false;
        }

        public void ZeroVectors()
        {
            currentDir = Vector2.zero;
            currentDir_Velocity = Vector2.zero;

            verticalVelocity = Vector3.zero;

            velocity = Vector3.zero;

            movementX = 0f;
            movementY = 0f;
        }

        public void ZeroCameraVectors()
        {
            cameraPitch = 0.0f;
            currentMouseDelta = Vector2.zero;
            currentMouseDelta_Velocity = Vector2.zero;
        }

        public void ResetBools()
        {
            isGrounded = true;
            isWalking = false;
            isRunning = false;
            isJumping = false;
            isFalling = false;
        }
    }
}