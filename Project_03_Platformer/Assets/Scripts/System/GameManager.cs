using System;
using System.Collections;
using TMPro;
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
                _GM = FindFirstObjectByType<GameManager>();
            }

            else if (_GM != this)
            {
                Destroy(gameObject);
            }

            return _GM;
        }

        public GameObject boundaries;

        [Header("Event System")]
        public float deathTime = 0.3f;

        [Header("Tick System")]
        public float TickDuration = 0.01f;

        [Header("Bools")]
        public bool cursorEnabled = true;
        public bool gamePaused = false;
        public bool gameRunning = false;


        void Awake()
        {
            GetGameManager();
        }


        void Start()
        {
            //subscribe to events
            //TickSystem.OnTickEvent += OnTick;

            // GameEvents._GE.onGameStartEvent += OnStart;
            // GameEvents._GE.onGameEndEvent += OnEnd;
            // GameEvents._GE.onGameRestartEvent += OnRestart;

            PlayerEvents._PE.onDeathEvent += OnDeathEvent;
            PlayerEvents._PE.onGoalEvent += OnGoalEvent;

            PlayerLocomotion._PL.hasControl = false;
            Time.timeScale = 0;
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


            if (Input.GetButtonDown("Cancel"))
            {
                if (gamePaused && gameRunning)
                {
                    UnpauseGame();
                }

                else if (gameRunning)
                {
                    PauseGame();
                }
            }


            //debug wipe high score
            // if (Input.GetKeyDown("p"))
            // {
            //     WipeScore();
            // }
        }

        public void PlayGame()
        {
            //give the player control
            PlayerLocomotion._PL.hasControl = true;
            Time.timeScale = 1;
            cursorEnabled = false;
            gameRunning = true;

            GameEvents.StartGame();
            TickSystem.TimerStart();
        }

        private void OnDeathEvent()
        {
            boundaries.SetActive(false);
            StartCoroutine("DeathEvents");
        }

        private IEnumerator DeathEvents()
        {
            // Debug.Log("GameManager: " + "Running death events...");
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(deathTime);
            Time.timeScale = 1f;

            PlayerLocomotion._PL.ResetPlayer();
            boundaries.SetActive(true);
        }

        private void OnGoalEvent()
        {
            Debug.Log("You win!");
            PlayerLocomotion._PL.hasControl = false;
            cursorEnabled = true;
            gameRunning = false;
            Time.timeScale = 0;

            PlayerSFX._SFX.PlayAudioClip(PlayerSFX._SFX.winClip, 1);

            TickSystem.TimerStop();
            GameEvents.EndGame();
        }

        void OnTick(int tick)
        {
            Debug.Log(tick);
            
        }


        public void PauseGame()
        {
            gamePaused = true;
            cursorEnabled = true;
            Time.timeScale = 0;
            GameEvents.PauseGame();
        }

        public void UnpauseGame()
        {
            gamePaused = false;
            cursorEnabled = false;
            Time.timeScale = 1;
            GameEvents.UnpauseGame();
        }


        public void RestartGame()
        {
            GameEvents.RestartGame();
            TickSystem.TimerReset();
            // Debug.Log("Restarting...");
            // UIHighScores.HighScoreChart.Hide();
            // TickSystem.TimerReset();
            // PlayerLocomotion._PL.ResetPlayer();
            // PlayerLocomotion._PL.hasControl = true;

            // SceneManager.LoadScene("TestSlide");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting the game!");
            Application.Quit();
        }

        public void WipeScore()
        {
            PlayerPrefs.SetFloat("Record", 999999);
        }
    }

}