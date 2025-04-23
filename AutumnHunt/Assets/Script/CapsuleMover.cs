using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CapsuleMover : MonoBehaviour
{
    
    public float playerSpeed = 3.0f;
    public float rotationSpeed;

    public float jumpSpeed;
    private float ySpeed;
    private CharacterController connect;
    public bool isGrounded;

    // private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //player is on the ground when game starts
        // isGrounded = true;

        // rb = gameObject.GetComponent<Rigidbody>();
        connect = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        moveDirection.Normalize();
        
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01 (magnitude);
        // transform.Translate(moveDirection * magnitude * playerSpeed * Time.deltaTime, Space.World);
        connect.SimpleMove(moveDirection * magnitude * playerSpeed); //moves player with speed

        //the physics of gravity "-9.81" will be assigned to the y speed multiplied by the timing of each frame
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump")) {
            //assign now as a constant rate when space is pressed
            ySpeed = -0.5f;
            isGrounded = false;
        }

        Vector3 playerVel = moveDirection * magnitude; //change in direction * it's magnitude per update = velocity
        // transform.Translate(playerVel * Time.deltaTime);
        connect.Move(playerVel * Time.deltaTime);

        if (connect.isGrounded)
        {
            ySpeed = -0.5f; 
            isGrounded = true;

            if (Input.GetButtonDown("Jump")) {
                ySpeed = jumpSpeed;
                isGrounded = false;
            }
        }

        if (moveDirection != Vector3.zero) {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
            
        
        // if (Input.GetButtonDown("Jump")) {
        //     rb.velocity = new Vector3(0f, 5f, 0f);
        //     Debug.Log("I am pressing the spacebar");
        // }
    }

    // void OnCollisionExit(Collision collision)
    // {
    //     collision.gameObject.CompareTag("ground");
    //     isGrounded = false;
    // }
}
