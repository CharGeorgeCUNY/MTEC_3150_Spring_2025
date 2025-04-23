using System;
using UnityEngine;

namespace WinterSym
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents _GE;

        public GameEvents GetGameEvents()
        {
            if (_GE == null)
            {
                _GE = GameObject.FindFirstObjectByType<GameEvents>();
            }

            else if (_GE != this)
            {
                Destroy(gameObject);
            }

            return _GE;
        }

        void Awake()
        {
            GetGameEvents();
        }


        public static event Action onGameStartEvent;
        public static event Action onGameEndEvent;
        public static event Action onGameRestartEvent;
        public static event Action onGamePauseEvent;
        public static event Action onGameUnpauseEvent;


        public static void StartGame()
        {
            if (onGameStartEvent != null)
            {
                onGameStartEvent();
            }
        }

        public static void EndGame()
        {
            if (onGameEndEvent != null)
            {
                onGameEndEvent();
            }
        }

        public static void RestartGame()
        {
            if (onGameRestartEvent != null)
            {
                onGameRestartEvent();
            }
        }

        public static void PauseGame()
        {
            if (onGamePauseEvent != null)
            {
                onGamePauseEvent();
            }
        }

        public static void UnpauseGame()
        {
            if (onGamePauseEvent != null)
            {
                onGameUnpauseEvent();
            }
        }
    }
}