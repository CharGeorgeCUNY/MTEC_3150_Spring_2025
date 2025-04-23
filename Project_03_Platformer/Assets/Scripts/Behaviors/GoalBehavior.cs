using System;
using UnityEngine;

namespace WinterSym
{
    public class GoalBehavior : MonoBehaviour
    {
        void OnTriggerEnter(Collider collision)
        {
            if (collision.GetComponent<Rigidbody>() == PlayerLocomotion._PL.rb)
            {
                // Debug.Log("GoalBehavior: " + "Collided with a " + collision.gameObject);
                PlayerEvents._PE.Win();
            }
        }
    }
}