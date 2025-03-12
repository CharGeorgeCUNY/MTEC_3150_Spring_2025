using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Control : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    //player inputs
    float yMove;
    float xMove;

    //flag states
    bool glideFlag;
    bool jumpFlag;
    bool movingFlag = false;
    bool isGrounded;
    bool isGliding;
    bool smallJump; //unlimited jump for testing

    public float speed = 20;
    public float jumpPower = 8;
    public int glidePower = 10; //used when gliding

    private int jumpCount = 0; //counter
    public int staminaCount = 0; //counter

    private int maxJumpCount = 2;
    private int maxStamina = 400;


    public enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        Gliding,
        Falling
    }

    public PlayerState currentState = PlayerState.Idle; // Default state

    /****************************************************  Start  ****************************************************/
    void Start()
    {
        //Initilize Rigidbody & and Animator once
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    /****************************************************  UPDATE  ****************************************************/
    void Update()
    {
        //players input 
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        movingFlag = xMove != 0; // checking if xMove is 0

        //check for jumps
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpFlag = true;
            jumpCount++;        
        }

        //check for test jumps 
        if (Input.GetKeyDown(KeyCode.L))
        {
            smallJump = true;
        }

        //check for gliding
        if (Input.GetKey(KeyCode.K))
        {
            glideFlag = true;
            //change drag for rb
            //add force to float but still fall, so fall slower/decrease the gravity
        }
    }


    /*****************************************************************************************************************
     **********************************************  FIXED UPDATE  ***************************************************
     *****************************************************************************************************************/
    private void FixedUpdate()
    {

        //float velocityY = rb.velocity.y; // Get vertical speed
        
        //if (jumpFlag) //Jumping 
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        //    currentState = PlayerState.Jumping;
        //    //JumpinJack();
        //}

        //else if (smallJump) //temp secondary jump without limits for debug purposes 
        //{

        //    JumpinJack();
        //}
        //else if (isGrounded && movingFlag) //on the ground moving (walking)
        //{
        //    //
        //    currentState = PlayerState.Walking;

        //}
        //else if (glideFlag) //&& isGliding gliding on air !isgrounded (gliding)
        //{
        //    currentState = PlayerState.Gliding;
        //    //WonderingCosmoz();
        //}
        //else if (!isGrounded) //falling
        //{
        //    currentState = PlayerState.Falling;
        //}

        HandleAnimation();
        PerformStateLogic();
       
    }

    private void PerformStateLogic()
    {
        if (jumpFlag || smallJump) // Handle jumping logic
        {
            currentState = PlayerState.Jumping;
            PerformJumping();
        }
        else if (glideFlag) // Handle gliding logic
        {
            currentState = PlayerState.Gliding;
            PerformGliding();
        }
        else if (isGrounded && movingFlag) // Handle walking logic
        {
            currentState = PlayerState.Walking;
            PerformWalking();
        }
        else if (!isGrounded) // Handle falling logic
        {
            currentState = PlayerState.Falling;
            PerformFalling();
        }
        else // Default to Idle
        {
            currentState = PlayerState.Idle;
            PerformIdle();
        }
    }

    /****************************************** PHYSICS LOGIC *************************************************/
    private void PerformWalking()
    {
        rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
    }

    private void PerformJumping()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        }            
    }

    private void PerformGliding()
    {
        // rb.gravityScale = 0.5f;
        rb.velocity = new Vector2(xMove * glidePower * speed * Time.deltaTime, rb.velocity.y);

    }

    private void PerformFalling()
    {
        //rb.gravityScale = 1f; // Restore gravity to normal when falling
    }

    private void PerformIdle()
    {
        // Ensure no movement when idle
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }

    /*****************************************************************************************************************
     ****************************************  Collisions enter and exist  *******************************************
     *****************************************************************************************************************/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if the object collided with is the ground & reset jump count if entered
        if (collision.collider.CompareTag("Ground"))
        {


            // reset countrs and flags
            jumpCount = 0;
            staminaCount = 0;
            isGrounded = true;
            jumpFlag = false;  
            glideFlag = false; 

            //if player move then play walking anim
            if (movingFlag)
            {
                currentState = PlayerState.Walking; // Set state to walking
            }
            else //else idle
            {
                currentState = PlayerState.Idle; // Set state to idle if not moving
            }
            HandleAnimation();  // Update animation based on state
            Debug.Log("Colliding with ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //check if the object collided with is the ground & reset flag press once exiting
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false; //reset grounded when exiting
            currentState = PlayerState.Falling; // Set state to falling
            HandleAnimation();
            Debug.Log("off the ground wondering the cosmoz");
        }
    }


    private void HandleAnimation()
    {

        // Reset all states first
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Gliding", false);
        animator.SetBool("Falling", false);

        switch (currentState)
        {
            case PlayerState.Idle:
                animator.SetBool("ground_move", false);
                animator.SetBool("air_move", false);
                break;

            case PlayerState.Walking:
                animator.SetBool("ground_move", true);
                animator.SetBool("air_move", false);
                break;

            case PlayerState.Jumping:
                animator.SetTrigger("jump");
                break;

            case PlayerState.Gliding:
                animator.SetBool("isGliding", true);
                break;

            case PlayerState.Falling:
                animator.SetBool("air_move", true);
                animator.SetBool("ground_move", false);
                break;


        }
    }

    private void JumpinJack()
    {
        float velocityY = rb.velocity.y; // Get vertical speed
        //animator.SetFloat("Vertical", rb.velocity.y);
        //animator.SetBool("jumping", true);
        //
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        smallJump = false;
        if (velocityY > 0)
        {
            Debug.Log("Jumping");
        }
        else if (velocityY < 0) //falling
        {
            Debug.Log("Falling");
            //animator.SetBool("falling", true);
            //
        }
    }
}

/**
 * if on ground and no xmove then idle
 * if on ground and x move then iswalking
 * if !isgrounded and Jumped then jumping anim
 * if !isgrounded and velocity is >0 then falling
 */
//assets
//project settings 
//packages + gitignore 
//avoid library and temp , logs, object