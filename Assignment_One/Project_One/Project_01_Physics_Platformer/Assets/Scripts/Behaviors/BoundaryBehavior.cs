using System;
using UnityEngine;

namespace PhysicsSym
{
    public class BoundaryBehavior : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Rigidbody2D>() == PlayerController.rb2D && PlayerEvents.isDead == false)
            {
                //kills the player if they go out of bounds
                PlayerEvents.currentPlayer.Die();
            }
        }
    }
}
