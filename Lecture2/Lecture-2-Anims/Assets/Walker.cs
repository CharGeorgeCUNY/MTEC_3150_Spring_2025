using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
    
  // Start is called before the first frame update
    public float speed = 3.0f;

public Vector2 Veloctiy;
bool IsWalking;
public Rigidbody2D rigidBody2D;
public Animator animator;
      void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Veloctiy = Vector2.zero;
       Veloctiy = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
       
        float NormalizedSpeed = Velocity.magnitude;

        if(NormalizedSpeed > .2f)
        {
            IsWalking = true;
        }
        {
            IsWalking = false;
        }
        AudioVelocityUpdateMode.Normalize();

        Veloctiy *= speed;
        rigidBody2D.velocity   = Velocity;
        
    }
}
    