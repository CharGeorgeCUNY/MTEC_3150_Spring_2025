using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Idle,
    Walking,
    Jumping,
    Gliding,
    Falling
}

public class MovementController : MonoBehaviour
{

    [Header("Movment Settings")]
    public float speed = 20f;
    public float jumpPower = 8f;
    public float glidePower = 30f;
    public int maxJumpCount = 2;

    [Header("Components")]
    public Rigidbody2D rb;

    //states
    private float xMove;
    private bool moved = false; //flag for moving
    private bool jumpFlag;
    private bool glideFlag;
    private bool isGrounded;
    private int jumpCount = 0;

    //public access to current state for other scripts
    public PlayerState CurrentState { get; private set; }
    private PlayerState previousState;

    //events for other scripts that are subscirbed to state changes
    public event System.Action<PlayerState> OnStateChanged;



    public float glidingLinearDragCoefficient = 0.2f; // Adjust this value

    void Start()
    {
        if (rb == null)// if compnent hasnt been assigned assign it yourself bc im not doing it + its a good practice :)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        CurrentState = PlayerState.Idle;
        previousState = CurrentState;
    }

    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal"); // get horizontal input from player
        if (xMove > 0 || xMove < 0) //if player moves on either direction then moved is true
        {
            moved = true;
        }

        //User pressed spacebar, flag jumping state, increase jump count
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpFlag = true;
            jumpCount++;
            Debug.Log("Jump Count: " + jumpCount);
        }

        //if K pressed = true then glideFlag = true
        glideFlag = Input.GetKey(KeyCode.K);
    }


    private void FixedUpdate()
    {
        //1st determine the state
        DetermineState();

        //Notift the fellow scripts of players changes IMMMEDEATLY!!!!!!
        if (CurrentState != previousState)
        {
            OnStateChanged?.Invoke(CurrentState);
            previousState = CurrentState; //update previous state when state changes
        }

        //perform the physics movement (execute the order)
        ExecutePhysics();
    }

    void DetermineState()
    {
        //Debug.Log("determining state");

        if (jumpFlag) // if player pressed jump then they jump
        {
            CurrentState = PlayerState.Jumping;
            jumpFlag = false;
            Debug.Log("JUMP STATE ACTIVATE");
        }
        else if (glideFlag && !isGrounded)  // if player input glide and theyre on air then theyre gliding
        {
            Debug.Log("GLIDEEE ACTIVATE");

            CurrentState = PlayerState.Gliding;
        }
        else if (CurrentState == PlayerState.Jumping && rb.velocity.y <= 0) // if player is on air and is going down then theyre falling. 
        {
            Debug.Log("FALLL ACTIVATE");

            CurrentState = PlayerState.Falling;
        }
        else if (isGrounded && moved) //if player on ground and they  moved then theyre walkin
        {
            Debug.Log("WALK ACTIVATE");

            CurrentState = PlayerState.Walking;
        }
        else if (isGrounded) //if no input and is on ground then idle around
        {
            //Debug.Log("IDLE ACTIVATE");

            CurrentState = PlayerState.Idle;
        }
    }
    void ExecutePhysics()
    {
        //reset gravity unless you gliding throught the cozmoz

        if (CurrentState != PlayerState.Gliding)
        {
            rb.gravityScale = 1f;
        }

        switch (CurrentState)
        {
            case PlayerState.Idle:
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                break;

            case PlayerState.Walking:
                rb.velocity = new Vector2(xMove * speed * Time.deltaTime, rb.velocity.y);
                moved = false;


                // Flip sprite based on movement direction
                if (xMove < 0) // Moving left by default
                {
                    transform.localScale = new Vector3(1, 1, 1); // Normal scale
                }
                else if (xMove > 0) // Moving right
                {
                    transform.localScale = new Vector3(-1, 1, 1); // Flipped scale
                }


                break;

            case PlayerState.Jumping:
                if (rb.velocity.y <= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                }
                Debug.Log("Activating JUMP STATE SIR!!!");
                //reset jumpflag after jumping
                //jumpFlag = false;
                break;

            case PlayerState.Gliding:
                //rb.gravityScale = 0.5f;
                //rb.angularDrag = 1f;
                //rb.velocity = new Vector2(xMove * glidePower, rb.velocity.y);
                // Apply drag directly to velocity
                float newXVelocity = xMove * glidePower * glidingLinearDragCoefficient;
                float newYVelocity = rb.velocity.y * glidingLinearDragCoefficient;


                rb.velocity = new Vector2(newXVelocity, newYVelocity);

                break;


            case PlayerState.Falling:
                //falling uses normal gravity which is already applied...
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //objects with the tag Ground
        if (collision.collider.CompareTag("Ground"))
        {
            //reset jumpcount and set ground flag to true
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //when you leave the ground
        if (collision.collider.CompareTag("Ground"))
        {
            //your no longer on the ground
            isGrounded = false;
        }
    }
}
