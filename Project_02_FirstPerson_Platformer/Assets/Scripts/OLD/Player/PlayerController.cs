using UnityEngine;

namespace JumpSym.Player
{
    public class Controller : MonoBehaviour
    {
        [Header("Components")]
        public static Controller _Controller;
        
        //player controller singleton
        public static Controller GetPlayer()
        {
            if (_Controller == null)
            {
                _Controller = GameObject.FindFirstObjectByType<Controller>();
            }

            return _Controller;
        }

        public static CharacterController characterController;
        public static Camera playerCamera;
        public static Animator animator;
        public static Interactions interactions;
        public static Locomotion locomotion;
        public static WorldScanner worldScanner;


        public static Vector3 rememberPoint;

        void Awake()
        {
            GetPlayer();
        }

        void Start()
        {
            //hides and keeps the cursor in the game
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //assign components
            characterController = gameObject.GetComponent<CharacterController>();
            playerCamera = Camera.main;
            animator = gameObject.GetComponent<Animator>();
            locomotion = gameObject.GetComponent<Locomotion>();
            interactions = gameObject.GetComponent<Interactions>();
            worldScanner = gameObject.GetComponent<WorldScanner>();

            //make sure the player is in control
            if (!interactions.inControl)
            {
                interactions.SetControl(true);
            }

            //set up functions
        }

        void Update()
        {  
            locomotion.isGrounded = locomotion.CheckGrounded();

            locomotion.Move();
            locomotion.MoveCamera();
            // locomotion.MoveClimbing();
            locomotion.PlayAnimations();

            interactions.DoAction();
            // interactions.LookingAt();

            //interactions.CheckRagdoll();

            worldScanner.ScanHang();
        }


        void OnCollisionEnter(Collision collision)
        {
            locomotion.isGrounded = locomotion.CheckGrounded();
        }
    }
}
