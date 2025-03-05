using System; //used for event systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsSym
{
    public class PlayerEvents : MonoBehaviour
    {
        public static PlayerEvents currentPlayer;

        public static bool isDead;

                //player event system singleton
        public static PlayerEvents GetPlayerEvents()
        {
            if (currentPlayer == null)
            {
                currentPlayer = GameObject.FindFirstObjectByType<PlayerEvents>();
            }
            return currentPlayer;
        }
        
        void Awake()
        {
            currentPlayer = this;
        }
        
        //Death event
        public event Action onDeathEvent;

        public void Die()
        {
            if (onDeathEvent != null)
            {
                onDeathEvent();
            }
            isDead = true;
            StartCoroutine("DelayedReset");
        }

        //Goal event
        public event Action onGoalEvent;

        public void Win()
        {
            if (onGoalEvent != null)
            {
                onGoalEvent();
            }
        }

        private IEnumerator DelayedReset()
        {
            yield return new WaitForSecondsRealtime(GameManager.deathTime);
            isDead = false;
        }
    }
}