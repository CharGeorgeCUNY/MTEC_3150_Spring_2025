using UnityEngine;


namespace CatSym
{
    public class EnablePhysicsOnContact : MonoBehaviour
    {
        public Rigidbody rb;

        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == GameManager.player)
            {
                Debug.Log("EnablePhysicsOnContact: " + "Collided with a Player!");
                rb.isKinematic = false;
            }
        }
    }
}