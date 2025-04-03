using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterSym
{
    public class GameManager : MonoBehaviour
    {
        [Header("Components")]
        public static GameManager _GM;

        public GameManager GetGameManager()
        {
            if (_GM == null)
            {
                _GM = GameObject.FindFirstObjectByType<GameManager>();
            }

            else if (_GM != this)
            {
                Destroy(gameObject);
            }

            return _GM;
        }

        public Canvas startButton;

        [Header("Event System")]
        public float deathTime = 0.3f;

        [Header("Tick System")]
        public float TickDuration = 0.01f;

        [Header("Bools")]
        public bool cursorEnabled;


        void Awake()
        {
            GetGameManager();
        }


        void Start()
        {
            cursorEnabled = true;

            //subscribe to events
            //TickSystem.OnTickEvent += OnTick;
            PlayerEvents._PE.onDeathEvent += OnDeathEvent;
            PlayerEvents._PE.onGoalEvent += OnGoalEvent;
        }

        void OnDisable()
        {
            PlayerEvents._PE.onDeathEvent -= OnDeathEvent;
            PlayerEvents._PE.onGoalEvent -= OnGoalEvent;
        }

        void Update()
        {
            if (cursorEnabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void PlayGame()
        {
            //give the player control
            PlayerLocomotion._PL.hasControl = true;
            startButton.enabled = false;
            cursorEnabled = false;

            TickSystem.TimerStart();

        }

        private void OnDeathEvent()
        {
            StartCoroutine("DeathEvents");
        }

        private IEnumerator DeathEvents()
        {
            // Debug.Log("GameManager: " + "Running death events...");
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(deathTime);
            Time.timeScale = 1f;

            PlayerLocomotion._PL.ResetPlayer();
        }

        private void OnGoalEvent()
        {
            PlayerLocomotion._PL.hasControl = false;
            TickSystem.TimerStop();
            cursorEnabled = true;

            //check if the current time < the 6th existing entry
            if (UIHighScores.HighScoreChart.CheckAgainstExistingEntries(UITimer._TM.currentTime))
            {
                //if we're at max capacity, delete the highest time 
                if (UIHighScores.HighScoreChart.highScoreEntries.Count == 6) UIHighScores.HighScoreChart.TossSixthEntry();

                UIHighScores.HighScoreChart.UpdateTempTime(UITimer._TM.currentTime, UITimer._TM.GetTime());
                UIHighScores.HighScoreChart.inputField.gameObject.GetComponent<UIRecordRecorder>().Display();
            }
            else UIHighScores.HighScoreChart.Display();
        }

        void OnTick(int tick)
        {
            Debug.Log(tick);
            
        }

        public void RestartGame()
        {
            // Debug.Log("Restarting...");
            // UIHighScores.HighScoreChart.Hide();
            // TickSystem.TimerReset();
            // PlayerLocomotion._PL.ResetPlayer();
            // PlayerLocomotion._PL.hasControl = true;

            SceneManager.LoadScene("TestSlide");
        }
    }

}