using System;
using System.Threading;
using TMPro;
using UnityEngine;

namespace WinterSym
{
    public class UITimer : MonoBehaviour
    {
        public static UITimer _TM;

        public UITimer GetTimer()
        {
            if (_TM == null)
            {
                _TM = GameObject.FindFirstObjectByType<UITimer>();
            }

            else if (_TM != this)
            {
                Destroy(gameObject);
            }

            return _TM;
        }
        private TMP_Text timerText; 
        public float currentTime = 0f;
        private bool isRunning;

        void Awake()
        {
            GetTimer();

            timerText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            TickSystem.TimerStartAction += OnTimerStart;
            TickSystem.TimerStopAction += OnTimerStop;
            TickSystem.TimerResetAction += OnTimerReset;
            TickSystem.OnTickEvent += OnTick;
        }

        private void OnDisable()
        {
            TickSystem.TimerStartAction -= OnTimerStart;
            TickSystem.TimerStopAction -= OnTimerStop;
            TickSystem.TimerResetAction -= OnTimerReset;
            TickSystem.OnTickEvent -= OnTick;
        }


        private void OnTimerStart()
        {
            isRunning = true;
        }

        private void OnTimerStop()
        {
            isRunning = false;
        }

        private void OnTimerReset()
        {
            currentTime = 0f;
        }

        private void OnTick(int i)
        {
            if (!isRunning) return;

            currentTime += GameManager._GM.TickDuration;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = timeSpan.ToString(format:@"mm\:ss\:ff");
        }

        public string GetTime()
        {
            return timerText.text;
        }
    }
}