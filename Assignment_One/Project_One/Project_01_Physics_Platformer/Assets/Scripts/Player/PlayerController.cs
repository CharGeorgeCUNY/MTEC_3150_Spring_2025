using System; //used for event systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsSym
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Variables")]

        //movement speed
        public float speed = 1f;

        //bounce force
        public float bounce = 10f;

        //gravity force
        public float gravity = 5f;
        
        private float horizontalAxis;


        public static Rigidbody2D rb2D { get; private set; }
        public static GameObject playerMarker { get; private set; }

        private Vector3 lastVelocity;
        private float lastDirection;

        void Awake()
        {
            //get the player's physics component
            rb2D = GetComponent<Rigidbody2D>();

            //get the marker's location
            playerMarker = GameObject.Find("Player_Marker");
        }

        void Start()
        {
            ResetPlayer();
            rb2D.AddForce(Vector2.down * gravity * 10, ForceMode2D.Impulse);
        }

        void Update()
        {
            //---MOVEMENT---

            //update lastVelocity
            lastVelocity = rb2D.linearVelocity;
            lastDirection = rb2D.transform.localScale.x; //should only ever be 1, -1, or 0


            // //get the direction the player wants to move
            horizontalAxis = Input.GetAxis("Horizontal");
        }

        void FixedUpdate()
        {
            rb2D.linearVelocity = new Vector2(horizontalAxis * speed, rb2D.linearVelocity.y);
        }
        

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, bounce);
            }
        }

        public static void ResetPlayer()
        {
            rb2D.transform.position = playerMarker.transform.position;
        }
    }   
}