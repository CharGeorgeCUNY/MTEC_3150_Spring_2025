using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;
    GameObject cam;

    private void Awake()
    {
        //cam = GameObject.Find("Camera");
        //cam.transform.parent = this.gameObject.transform;
        //cam.transform.position = 
       
    }
    void Update()
    {
        //Assuming this is 3rd-person movement and the default Input Manager configuration is used.
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        //Use the value of "sprintSpeed" if left-shift is held down, otherwise use the value of "moveSpeed";
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        //Update the GameObject's position with the detected move direction and speed.
        transform.position += moveDirection * speed * Time.deltaTime;
    }
    

}
