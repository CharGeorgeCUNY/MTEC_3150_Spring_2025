using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CatSym

{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody rb;
        public PlayerScanner scanner;
        public Transform cameraTransform;
        public Transform playerTransform;
        public Animator animator;

        public float speed = 2f;
        public float jumpHeight = 20f;

        public float vertical;
        public float horizontal;
        public Vector3 movement = Vector3.zero;
        public Vector3 currentEulerAngles;

        public bool isMoving;

        public bool isGrounded;

        public bool isJumping;
        public bool isFalling;

        void Update()
        {
            Animate();
            CheckGrounded();
            GetInput();
        }

        void FixedUpdate()
        {
            Move();
        }


        private void CheckGrounded()
        {
            //----GROUND CHECK----
            //find if grounded using the PlayerScanner
            if (scanner.ScanFloor().HitFound || rb.linearVelocity.y == 0f)
            {
                isGrounded = true;
            }

            else
            {
                isGrounded = false;
            }
        }


        private void GetInput()
        {
            //walking
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            //set movement variable
            if (Math.Abs(vertical) != 0 || Math.Abs(horizontal) != 0)
            {
                isMoving = true;
            }

            else isMoving = false;

            //jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isJumping = true;
                animator.SetTrigger("Jumping");
            }

            if (isFalling && isGrounded)
            {
                isFalling = false;
                animator.SetTrigger("Landing");
            }
        }

        private void Move()
        {
            //movement calculations
            movement = playerTransform.forward * vertical + playerTransform.right * horizontal;
            movement.Normalize();

            if (isGrounded)
            {
                //reset velocity
                rb.linearVelocity = Vector3.zero;

                //add jump velocity
                if (isJumping)
                {
                    rb.linearVelocity += Vector3.up * jumpHeight;
                }

                //add movement velocity
                rb.linearVelocity += movement * speed;
            }

            else
            {
                if (isMoving)

                    //slow in-air movement
                    rb.linearVelocity += movement / 5f;
            }

            //turn player in direction of the camera
            playerTransform.localEulerAngles = new Vector3(0, cameraTransform.localEulerAngles.y, 0);
        }

        private void Animate()
        {
            //----ANIMATIONS----
            //report x and y trajectory to the animator
            animator.SetFloat("MovingX", horizontal, 0.1f, Time.deltaTime);
            animator.SetFloat("MovingY", vertical, 0.1f, Time.deltaTime);

            animator.SetBool("IsMoving", isMoving);
        }
    }
}