using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rbPlayer;
    //Vector3 move;

    float speedDefault = 10.0f;
    float speedMid = 9.0f;
    float speedHigh = 13.0f;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rbPlayer.velocity = Vector3.forward * speedDefault;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rbPlayer.velocity = Vector3.forward * speedDefault;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rbPlayer.velocity = Vector3.right * 2.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rbPlayer.velocity = Vector3.left * 2.0f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rbPlayer.velocity = -Vector3.forward * speedDefault;
        }

    }
}
