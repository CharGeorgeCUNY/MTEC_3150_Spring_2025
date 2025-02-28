using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    public Vector2 Veloctiy;
    public Rigidbody2D rigidBody2D;
    Animator ani;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Veloctiy = Vector2.zero;
       Veloctiy = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Veloctiy.Normalize();
        Veloctiy *= speed;
        rigidBody2D.velocity   = Veloctiy;
        ani.SetFloat("Horz", Veloctiy.x);
        
        ani.SetFloat("Vert", Veloctiy.y);


        
    }
}
    