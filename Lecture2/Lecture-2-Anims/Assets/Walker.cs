using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    public Vector2 Velocity;
    public Rigidbody2D rigidBody2D;
    public Animator animator;
    //bool isWalking;
    //bool movDown;
    //bool movUp;
    //bool movRight;
    //bool movLeft;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator =GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Velocity = Vector2.zero;
       Velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        float NormalizedSpeed = Velocity.magnitude;

        //if (NormalizedSpeed > .2f)
        //{
        //    isWalking = true;
        //}
        //else
        //{
        //    isWalking = false;
        //}

        //if (Input.GetAxis("Horizontal"), )
        Velocity.Normalize();
        
        //animator.SetBool("isWalking", isWalking);

        Velocity *= speed;
        rigidBody2D.velocity   = Velocity;

        animator.SetFloat("Horizontal", Velocity.x);
        animator.SetFloat("Vertical", Velocity.y);
    }
}
    