using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float rotationSpeed = 100f;
    private float currentRotation = 0f;

    void LateUpdate()
    {
        // Rotate camera with Left and Right Arrow keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }

        // Apply rotation to the camera
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 rotatedOffset = rotation * offset;
        transform.position = player.position + rotatedOffset;
        transform.LookAt(player.position);
    }
}