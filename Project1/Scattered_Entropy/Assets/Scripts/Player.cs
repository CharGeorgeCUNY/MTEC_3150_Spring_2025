using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 3.0f;
    public Vector2 Velocity;
    public Rigidbody2D rigidBody2D;
    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // the velocity is initialized to zero and changes from -1 to 1 
        // depending on the input called from the axis manager (inputs uses vectors)
        Velocity = Vector2.zero;
        Velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //normalizes the velocity's speed to -1 or 1
        Velocity.Normalize();
        
        Velocity *= Speed;
        rigidBody2D.velocity = Velocity;
    }
}
