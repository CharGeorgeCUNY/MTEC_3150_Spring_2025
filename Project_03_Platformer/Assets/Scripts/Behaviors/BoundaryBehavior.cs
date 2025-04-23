using System;
using System.Collections;
using UnityEngine;

namespace WinterSym
{
    public class BoundaryBehavior : MonoBehaviour
    {

        void OnTriggerEnter(Collider collision)
        {
            if (collision.GetComponent<Rigidbody>() == PlayerLocomotion._PL.rb && PlayerEvents.isDead == false)
            {
                Debug.Log("BoundaryBehaviour: " + "Collided with a " + collision.gameObject);

                StartCoroutine(WaitThenKillPlayer());

                if (!PlayerSFX._SFX.myAudioSource.isPlaying)
                {
                    MusicPlayer._MP.FadeMusic();
                    PlayerSFX._SFX.PlayAudioClip(PlayerSFX._SFX.dieClip, 0.5f);
                }
            }
        }


        private IEnumerator WaitThenKillPlayer()
        {
            yield return new WaitForSeconds(PlayerSFX._SFX.dieClip.length);

            //kills the player if they go out of bounds
            PlayerEvents._PE.Die();
        }

    }
}