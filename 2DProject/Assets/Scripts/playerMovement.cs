using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    SpriteRenderer sr;

    public float speed = 3.0f;
    public float jumpSpeed = 5.0f;

    Vector2 velocity;
    bool isMoving;

    //note: could really use a jumping limitation 
    bool canJump;
    public RaycastHit2D hit;
    bool groundCheck = true;
    //int count = 0;

    //game over
    string currentSceneName;

    //platform
    //Platform platform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
       
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector2.zero;
        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //float NormalizedSpeed = velocity.magnitude;

        hit = Physics2D.Raycast(transform.position, -Vector2.up * 1.18f);
        Debug.DrawRay(transform.position, -Vector2.up * 1.18f,Color.red);


        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))

        {
            if (Input.GetKey(KeyCode.LeftArrow)){
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            isMoving = true;
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            //isMoving = false;
            //animator.SetBool("isMoving", isMoving);

        }
        if (Input.GetKey(KeyCode.Space) && groundCheck)
        {
            canJump = true;
            animator.SetBool("canJump", canJump);
        }
        else
        {
            canJump = false;
            animator.SetBool("canJump", canJump);
        }
        velocity.Normalize();
        velocity *= speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "harzard")||(collision.gameObject.tag == "enterRock"))
        {
            //gameObject.SetActive(false);
            GameOver();
        }
    }
    //playmovement Specific
    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
        //jumpScript
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    //note Scene resets when deathSpike,startRock, and endRock.
    void GameOver()
    {
        SceneManager.LoadScene(currentSceneName);
    }
}
