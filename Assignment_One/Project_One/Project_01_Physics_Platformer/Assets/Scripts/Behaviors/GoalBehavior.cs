using System;
using UnityEngine;

namespace PhysicsSym
{
    public class GoalBehavior : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Rigidbody2D>() == PlayerController.rb2D)
            {
                PlayerEvents.currentPlayer.Win();
            }
        }
    }
}
