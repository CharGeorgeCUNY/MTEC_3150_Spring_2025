using System;
using System.Collections;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace WinterSym
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [Header("Components")]
        public Animator animator;
        public Transform playerCamera;
        public PlayerScanner scanner;
        public Rigidbody rb;

        public static PlayerLocomotion _PL;

        public PlayerLocomotion GetPlayerLocomotion()
        {
            if (_PL == null)
            {
                _PL = GameObject.FindFirstObjectByType<PlayerLocomotion>();
            }

            else if (_PL != this)
            {
                Destroy(this.gameObject);
            }

            return _PL;
        }


        [Header("Camera Control")]
        public float mouseSensitivity = 3.5f;
        public float cameraPitch { get; private set; } = 0.0f;
        public float cameraYaw { get; private set; } = 0.0f;


        [Header("Movement Control")]
        public float moveSpeed = 6.0f;
        public float currentSpeed = 0.0f;

        public float slideMinVelocity = 3.0f;
        public float fallMinVelocity = 2.0f;
        public float maxVelocity = 10.0f;


        [Range(0.0f, 0.5f)] public float smoothing = 0.1f;

        [Range(0.0f, 0.5f)] public float mouseSmoothing = 0.03f;


        [Header("Movement Vectors")]
        private Vector2 currentDir = Vector2.zero;
        private Vector2 currentDir_Velocity = Vector2.zero;

        private Vector2 targetInput = Vector2.zero;
        private Vector3 targetDir = Vector3.zero;

        private Vector2 currentMouseDelta;
        private Vector2 currentMouseDelta_Velocity = Vector2.zero;

        public Vector3 verticalVelocity = Vector3.zero;

        public Vector3 velocity = Vector3.zero;
        public Vector3 lastVelocity = Vector2.zero;


        [Header("Physics")]
        public float jumpHeight = 5.0f;
        public float gravity = -9.81f;
        public float terminalVelocity = -100f;

        [Range(0, 1)] public float airDrag = 0.1f;
        [Range(0, 1)] public float sliding;
        [Range(0, 10)] public float maxMagnitude = 6f;

        [Header("State Bools")]
        public bool isGrounded = true;
        public bool isOnIce = false;

        public bool isMoving;
        public bool isSliding;
        public bool isJumping;
        public bool isFalling;
        public bool isTripping;

        public bool hasControl;


        [Header("Animation Data")]
        public float movementX;
        public float movementY;

        [Header("Constant Variables")]
        public Vector3 startPos;
        public Quaternion startRot;


        void Awake()
        {
            GetPlayerLocomotion();
        }

        void Start()
        {
            currentSpeed = moveSpeed;

            GameEvents.onGameRestartEvent += OnRestartEvent;

            PlayerEvents._PE.onGoalEvent += OnGoalEvent;
            PlayerEvents._PE.onDeathEvent += OnDeathEvent;
        }

        void OnEnable()
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }

        void OnDisable()
        {
            PlayerEvents._PE.onGoalEvent -= OnGoalEvent;
            PlayerEvents._PE.onDeathEvent -= OnDeathEvent;
        }

        void Update()
        {
            MoveCamera();
            CheckConditions();
            CheckGrounded(); 
        }

        void LateUpdate()
        {
            if (!isTripping) CheckGrounded();
            if (!isTripping) Move();
        }

        public void Move()
        {
            if (!hasControl) return;
            
            //----MOVING----
            //Create a new vector3 from input
            targetInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //set movement variable
            if (Math.Abs(Input.GetAxis("Horizontal")) != 0 || Input.GetAxis("Vertical") != 0)
            {
                isMoving = true;
            }

            else isMoving = false;


            targetDir = transform.forward * targetInput.y + transform.right * targetInput.x;

            targetDir.Normalize();

            //multiply input by player speed and time passed. make sure we have enough boost!
            velocity = targetDir * currentSpeed * Time.deltaTime * 1000;

            //add force in velocity direction to player RB
            rb.AddForce(velocity);
            

            //----ANIMATIONS----
            //report x and y trajectory to the animator
            movementX = targetInput.x;
            movementY = targetInput.y;
            
            animator.SetFloat("MovingX", movementX);
            animator.SetFloat("MovingY", movementY);

            animator.SetBool("IsGrounded", isGrounded);

            animator.SetBool("IsMoving", isMoving);
            animator.SetBool("IsSliding", isSliding);
            animator.SetBool("IsJumping", isJumping);
            animator.SetBool("IsFalling", isFalling);
            animator.SetBool("IsTripping", isTripping);
        }

        public void CheckConditions()
        {
            //if you're not moving but have velocity above move speed, slide
            if (Math.Abs(rb.linearVelocity.x) > slideMinVelocity || Mathf.Abs(rb.linearVelocity.z) > slideMinVelocity)
            {
                if (!isMoving && !isFalling && !isTripping) isSliding = true;
                else isSliding = false;
            }
            
            else isSliding = false;
            

            //if you have vertical velocity > than min velocity to fall
            if(Math.Abs(rb.linearVelocity.y) > fallMinVelocity)
            {
                isFalling = true;

                //make sure we're not moving or sliding in the air!
                isMoving = false;
                isSliding = false;
            }

            //if you're going too fast, fall over (unless youre sliding, or falling)
            if (Mathf.Abs(rb.linearVelocity.x) > maxVelocity || Mathf.Abs(rb.linearVelocity.z) > maxVelocity)
            {
                if (isMoving && !isSliding && !isFalling && isGrounded) StartTripping();
            }
        }

        void StartTripping()
        {
            Debug.Log("RIP");
            PlayerSFX._SFX.PlayAudioClip(PlayerSFX._SFX.slipClip, 1f);

            isMoving = false;
            isTripping = true;

            animator.SetTrigger("Tripping");
            animator.Update(0);

            rb.isKinematic = true;
        }

        public void StopTripping()
        {
            targetInput = Vector2.zero;

            movementX = 0;
            movementY = 0;

            animator.SetFloat("MovingX", 0);
            animator.SetFloat("MovingY", 0);

            rb.isKinematic = false;

            isTripping = false;
        }


        public void MoveCamera()
        {
            if (!hasControl) return;
            //get the location (x, y) of the mouse
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDelta_Velocity, mouseSmoothing);

            //rotate on the x axis up/down, within range (-75, 75)
            cameraPitch -= currentMouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -75, 75);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;

            cameraYaw += currentMouseDelta.x * mouseSensitivity;

            if (!isTripping) transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }

        public void CheckGrounded()
        {
            //----GROUND CHECK----
            //check if we are grounded using ScanFloor(), but only if we aren't jumping
            if (!isJumping)
            {
                //find if grounded using virtual floor
                if (scanner.ScanFloor().HitFound)
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
        }

        public bool HasFlags(GameObject myObject)
        {
            Debug.Log("PlayerLocomotion: Checking for flags...");
            if (myObject.GetComponent<UtilityFlags>() != null) return true;
            else return false;
        }

        public void ResetPlayer()
        {
            if (isTripping) StopTripping();
            if (isFalling) isFalling = false;
            rb.transform.position = startPos;
            rb.transform.rotation = startRot;
        }

        private void OnDeathEvent()
        {
            rb.linearVelocity = Vector3.zero;
            ResetPlayer();
        }

        private void OnGoalEvent()
        {
            rb.linearVelocity = Vector3.zero;
        }

        private void OnRestartEvent()
        {            
            rb.linearVelocity = Vector3.zero;
            ResetPlayer();
        }
    }
}
