using System;
using TMPro;
using UnityEngine;

namespace WinterSym
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _UI;

        public UIManager GetHighScores()
        {
            if (_UI == null)
            {
                _UI = GameObject.FindFirstObjectByType<UIManager>();
            }

            else if (_UI != this)
            {
                Destroy(gameObject);
            }

            return _UI;
        }

        [Header("Components")]
        public GameObject startPanel;
        public GameObject scorePanel;
        public GameObject pausePanel;

        public TMP_Text highscoreText;
        public TMP_Text currentscoreText;


        [Header("Scoring")]
        public float recordTime;
        public float currentTime;

        void Awake()
        {
            GetHighScores();
        }

        void Start()
        {
            GameEvents.onGameStartEvent += OnGameStart;
            GameEvents.onGameEndEvent += OnGameEnd;
            GameEvents.onGameRestartEvent += OnGameRestart;
            GameEvents.onGamePauseEvent += OnGamePause;
            GameEvents.onGameUnpauseEvent += OnGameUnpause;
            
            scorePanel.SetActive(false);
            startPanel.SetActive(true);
            pausePanel.SetActive(false);
        }

        void OnDisable()
        {
            GameEvents.onGameStartEvent -= OnGameStart;
            GameEvents.onGameEndEvent -= OnGameEnd;
            GameEvents.onGameRestartEvent -= OnGameRestart;
        }

        private void OnGameStart()
        {
            startPanel.SetActive(false);
            pausePanel.SetActive(false);
        }

        private void OnGameEnd()
        {
            CheckHighScore();
            SetHighScore();

            scorePanel.SetActive(true);
        }

        private void OnGameRestart()
        {
            scorePanel.SetActive(false);
            startPanel.SetActive(true);
            pausePanel.SetActive(false);
        }

        private void OnGamePause()
        {
            pausePanel.SetActive(true);
        }

        private void OnGameUnpause()
        {
            pausePanel.SetActive(false);
        }

        public void CheckHighScore()
        {
            //if there is no record, set it to the max time
            if (!PlayerPrefs.HasKey("Record")) PlayerPrefs.SetFloat("Record", 999999);

            //get high score string from playerprefs
            recordTime = PlayerPrefs.GetFloat("Record");

            //compare to current player's high score
            currentTime = UITimer._TM.currentTime;

            //if current score < high score, replace high score with current score
            if (currentTime < recordTime)
            {
                PlayerPrefs.SetFloat("Record", currentTime);
            }
        }

        public void SetHighScore()
        {
            TimeSpan RecordTimeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Record"));
            highscoreText.text = RecordTimeSpan.ToString(format: @"mm\:ss\:ff");

            TimeSpan currentTimeSpan = TimeSpan.FromSeconds(currentTime);
            currentscoreText.text = currentTimeSpan.ToString(format: @"mm\:ss\:ff");
        }

    }
}
