using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePillar : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the object around its Y-axis (you can change the axis if needed)
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
