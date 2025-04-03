using System; //used for event systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterSym
{
    public class PlayerEvents : MonoBehaviour
    {
        public static PlayerEvents _PE;

        public static bool isDead;

                //player event system singleton
        public PlayerEvents GetPlayerEvents()
        {
            if (_PE == null)
            {
                _PE = GameObject.FindFirstObjectByType<PlayerEvents>();
            }
            else if (_PE != this) Destroy(this.gameObject);

            return _PE;
        }
        
        void Awake()
        {
            GetPlayerEvents();
        }
        
        //Death event
        public event Action onDeathEvent;

        public void Die()
        {
            // Debug.Log("PlayerEvents: " + "Killing player...");
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
            // Debug.Log("PlayerEvents: " + "Player won!");
            if (onGoalEvent != null)
            {
                onGoalEvent();
            }
        }

        private IEnumerator DelayedReset()
        {
            yield return new WaitForSecondsRealtime(GameManager._GM.deathTime);
            isDead = false;
        }
    }
}