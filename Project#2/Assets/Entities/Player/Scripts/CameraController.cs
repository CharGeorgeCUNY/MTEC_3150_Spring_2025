using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensX = 400;
    public float sensY = 400;

    public Transform cam;
    public Transform orientation;

    private float mouseX;
    private float mouseY;

    private float multiplier = 0.01f;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ProcessInputs();

        cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    private void ProcessInputs()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        rotationY += mouseX * sensX * multiplier;
        rotationX -= mouseY * sensY * multiplier;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }
}
