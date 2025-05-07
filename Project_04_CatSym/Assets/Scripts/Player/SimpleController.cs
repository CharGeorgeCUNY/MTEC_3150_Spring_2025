using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 10f;


    public float forward;
    public float right;
    public Vector3 movement = Vector3.zero;

    void Update()
    {
        rb.linearVelocity = Vector3.zero;
        GetInput();
    }

    void FixedUpdate()
    {
        Move(movement);
    }


    private void GetInput()
    {
        forward = Input.GetAxisRaw("Vertical");
        right = Input.GetAxisRaw("Horizontal");

        movement = new Vector3(right, 0, forward);
        movement = movement.normalized;
    }

    private void Move(Vector3 movement)
    {
        rb.linearVelocity = movement * speed * Time.deltaTime * 10;
    }

}
