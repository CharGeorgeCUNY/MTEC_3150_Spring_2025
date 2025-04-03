using System.Collections;
using JumpSym.Utility;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using UnityEngine;

namespace WinterSym
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [Header("Components")]
        public Animator animator;
        public Camera playerCamera;
        public PlayerScanner scanner;
        public Rigidbody rb;
        public VirtualFloor virtualFloor;
        public SkinnedMeshRenderer head;
        public AudioSource slipSoundEffect;

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
        public float walkSpeed = 6.0f;
        public float currentSpeed = 0.0f;

        public float maxVelocity = 10.0f;


        [Range(0.0f, 0.5f)] public float smoothing = 0.1f;

        [Range(0.0f, 0.5f)] public float mouseSmoothing = 0.03f;


        [Header("Movement Vectors")]
        private Vector2 currentDir = Vector2.zero;
        private Vector2 currentDir_Velocity = Vector2.zero;

        private Vector3 targetInput = Vector3.zero;
        private Vector3 targetDir = Vector3.zero;

        private Vector2 currentMouseDelta = Vector2.zero;
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

        public bool isWalking;
        public bool isRunning;
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
            
            hasControl = false;
        }

        void Start()
        {
            PlayerEvents._PE.onGoalEvent += OnGoalEvent;
        }

        void OnEnable()
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }

        void OnDisable()
        {
            PlayerEvents._PE.onGoalEvent -= OnGoalEvent;
        }

        void Update()
        {
            MoveCamera();
            if (!isTripping) PlayAnimations();
        }

        void LateUpdate()
        {
            if (!isTripping) CheckGrounded();
            if (!isTripping) Move();
        }


        public void Move()
        {
            if (!hasControl) return;
            // Debug.Log("PlayerLocomotion: " + "Moving player...");
            //----MOVING----
            //if jumping or falling, decrease momentum slightly
            // if (isJumping || isFalling)
            // {
            //     targetDir.x *= airDrag;
            //     targetDir.y *= airDrag;
            // }

            //Create a new vector3 from input
            targetInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir = transform.forward * targetInput.y + transform.right * targetInput.x;

            targetDir.Normalize();

            //multiply input by player speed and time passed. make sure we have enough boost!
            velocity = targetDir * currentSpeed * Time.deltaTime * 1000;

            //add force in velocity direction to player RB
            rb.AddForce(velocity);

            // Debug.Log(rb.linearVelocity);

            //if you're going too fast, fall over
            if (Mathf.Abs(rb.linearVelocity.x) > maxVelocity || Mathf.Abs(rb.linearVelocity.z) > maxVelocity)
            {
                StartCoroutine(StartTripping());
            }


            // //stop sliding on floors            
            // if (targetInput.x == 0 && scanner.GetGroundFlagType() != GroundFlagType.Ice)
            // {
            //     rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, rb.linearVelocity.z);
            // }

            // if (targetInput.y == 0 && scanner.GetGroundFlagType() != GroundFlagType.Ice)
            // {
            //     rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0);
            // }


            //----ANIMATIONS----
            //report x and y trajectory to the animator
            movementX = targetInput.x;
            movementY = targetInput.y;

            //check if we're idle or moving
            if (targetDir.x != 0 || targetDir.z != 0)
            {
                //if we are just starting to walk, set walking to true and set base speed
                if (!isWalking)
                {
                    currentSpeed = walkSpeed;
                    isWalking = true;
                }
            }

            //if the player isnt moving
            else isWalking = false;


            // ----JUMPING----
            //if we are grounded, check if the player has tried to jump
            if (isGrounded && !isJumping && Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping!");
                //trigger jumping animation and switch to state isJumping
                animator.SetTrigger("Jumping");
                isJumping = true;
                isGrounded = false;

                //also make sure you actually jump up
                rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
                //wait until we have started falling and switch to state isFalling
                StartCoroutine(StartFalling());
            }

            //set isFalling to false if we have landed
            if (isFalling && isGrounded)
            {
                isFalling = false;
            }

        }

        IEnumerator StartFalling()
        {
            Debug.Log("The player has started falling.");
            yield return new WaitForSeconds(0.06f);
            isJumping = false;
            isFalling = true;
        }

        IEnumerator StartTripping()
        {
            Debug.Log("RIP");

            slipSoundEffect.Play();

            isWalking = false;
            isTripping = true;

            // Debug.Log("PlayerLocomotion: " + "Player is tripping: " + isTripping + " Player is walking: " + isWalking);

            animator.SetBool("IsWalking", false);
            animator.SetTrigger("Tripping");

            head.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

            rb.linearVelocity = Vector3.zero;


            yield return new WaitForSeconds(5.0f);

            head.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

            targetInput = Vector2.zero;

            movementX = 0;
            movementY = 0;

            animator.SetFloat("MovingX", 0);
            animator.SetFloat("MovingY", 0);

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
            playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;

            cameraYaw += currentMouseDelta.x * mouseSensitivity;

            // Debug.Log(Vector3.up * cameraYaw);

            transform.localEulerAngles = Vector3.up * cameraYaw;
        }


        public void PlayAnimations()
        {
            animator.SetFloat("MovingX", movementX);
            animator.SetFloat("MovingY", movementY);

            animator.SetBool("IsWalking", isWalking);

            animator.SetBool("IsFalling", isFalling);
            animator.SetBool("IsGrounded", isGrounded);

        }

        public void CheckGrounded()
        {
            //----GROUND CHECK----
            //check if we are grounded using ScanFloor(), but only if we aren't jumping
            if (!isJumping)
            {
                // Debug.Log("PlayerLocomotion: " + scanner);
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

                // Debug.Log("PlayerLocomotion: Player is grounded = " + isGrounded);
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
            rb.transform.position = startPos;
            rb.transform.rotation = startRot;
        }

        private void OnGoalEvent()
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
