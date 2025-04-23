using UnityEngine;

public class RampBehavior : MonoBehaviour
{
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Player_Controller")
        {
            collision.rigidbody.AddForce(Physics.gravity);
        }
    }
}
