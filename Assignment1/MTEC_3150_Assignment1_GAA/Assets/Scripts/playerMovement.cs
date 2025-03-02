using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool isRunning;
    private bool isJumping;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        isRunning = false;
        isJumping = false;
    }


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        isRunning = true;



        //FLipping the player when moving horizontally
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = false;
            isJumping = true;

            body.velocity = new Vector2(body.velocity.x, speed / 2f);
        }
        
    }
}
