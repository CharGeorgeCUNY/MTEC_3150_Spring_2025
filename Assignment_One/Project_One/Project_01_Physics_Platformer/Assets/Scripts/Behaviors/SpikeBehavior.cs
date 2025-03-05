using UnityEngine;

namespace PhysicsSym
{
    public class SpikeBehavior : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && PlayerEvents.isDead == false)
            {
                PlayerEffects.myContact = collision.GetContact(0);
                PlayerEffects.currentEffects.DeathSparkles();
                PlayerEvents.currentPlayer.Die();
            }
        }
    }
}
