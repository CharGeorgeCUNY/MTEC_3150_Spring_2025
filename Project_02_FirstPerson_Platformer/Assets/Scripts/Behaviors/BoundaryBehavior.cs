using System;
using UnityEngine;

namespace WinterSym
{
    public class BoundaryBehavior : MonoBehaviour
    {
        void OnTriggerEnter(Collider collision)
        {
            if (collision.GetComponent<Rigidbody>() == PlayerLocomotion._PL.rb && PlayerEvents.isDead == false)
            {
                // Debug.Log("BoundaryBehaviour: " + "Collided with a " + collision.gameObject);
                //kills the player if they go out of bounds
                PlayerEvents._PE.Die();
            }
        }
    }
}