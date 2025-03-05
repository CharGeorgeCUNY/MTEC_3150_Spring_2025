using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsSym
{
    public class GameManager : MonoBehaviour
    {
        //singleton assignment
        static GameManager _Instance;

        [SerializeField]
        public static float TickDuration { get; private set; } = 0.1f;

        public static float deathTime { get; private set; } = 2.5f;

        //game manager singleton
        public static GameManager GetGameManager()
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindFirstObjectByType<GameManager>();
            }
            return _Instance;
        }

        void Start()
        {
            //subscribe to various events
            //watch for player death and restart
            PlayerEvents.currentPlayer.onDeathEvent += OnDeathEvent;

            //watch for goal and if so continue to next level
            PlayerEvents.currentPlayer.onGoalEvent += OnGoalEvent;
        }

        private void OnDeathEvent()
        {
            StartCoroutine("DeathEvents");
        }

        private IEnumerator DeathEvents()
        {
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(deathTime);
            Time.timeScale = 1f;

            PlayerController.ResetPlayer();
        }

        private void OnGoalEvent()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.GetSceneByBuildIndex(nextSceneIndex) != null)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }

            else
            {
                Debug.Log("Next scene is null!");
            }
        }
    }
}