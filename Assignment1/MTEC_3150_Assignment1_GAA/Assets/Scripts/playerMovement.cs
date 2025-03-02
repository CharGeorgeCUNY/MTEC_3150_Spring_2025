using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    /* Unlike, p5.js, these are mae irrelevent by the GUI
    private bool isRunning;
    private bool isJumping;
    */

    private bool grounded;

    private Rigidbody2D body;
    private Animator animation;

    private void Awake()
    {
        //These help you grab references from things in the engine.
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();


        //
        //isRunning = false;
        //isJumping = false;
    }


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        //isRunning = true;



        //FLipping the player when moving horizontally
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 1f);
        }


        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {

            //isRunning = false;
            //isJumping = true;

            Jump();
        }


        // Is the input 0? No? Then start runnin'! Is the player touching the ground? Get ready to jump!
        animation.SetBool("run", horizontalInput != 0);
        animation.SetBool("touchGround", grounded == true);
    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        animation.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            grounded = true;
        }
    }
}
