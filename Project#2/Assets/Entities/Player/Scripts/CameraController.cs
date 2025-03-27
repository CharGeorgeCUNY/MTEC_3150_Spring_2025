using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform revolver;

    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;

    float rotationX = 0f;
    float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Rotate on X axis (pitch)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30f, 90f);
      

        // Rotate on Y axis (yaw)
        rotationY += mouseX;

        // Apply rotations
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); // Camera rotation
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); // Orientation rotation
    }
}
