using UnityEngine;

public class SimpleController : MonoBehaviour
{
    private Vector3 targetDir = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float currentSpeed = 5.0f;
    public Rigidbody rb;


    void FixedUpdate()
    {
        //MOVEMENT
        //Create a new vector3 from input
        Vector2 targetInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir = transform.forward * targetInput.y + transform.right * targetInput.x;

        targetDir.Normalize();

        //multiply input by player speed and time passed. make sure we have enough boost!
        velocity = targetDir * currentSpeed * Time.deltaTime * 1000;

        //add force in velocity direction to player RB
        rb.AddForce(velocity);
    }
}
