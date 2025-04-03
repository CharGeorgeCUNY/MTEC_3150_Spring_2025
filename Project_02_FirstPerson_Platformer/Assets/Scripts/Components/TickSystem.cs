using System;
using UnityEngine;

namespace WinterSym
{
    public class TickSystem : MonoBehaviour
    {
        [Header("Components")]
        public static TickSystem _TS;

        public TickSystem GetTimer()
        {
            if (_TS == null)
            {
                _TS = GameObject.FindFirstObjectByType<TickSystem>();
            }

            else
            {
                Destroy(this.gameObject);
            }

            return _TS;
        }

        public static event Action TimerStartAction;
        public static event Action TimerStopAction;
        public static event Action TimerResetAction;

        public static void TimerStart() => TimerStartAction?.Invoke();
        public static void TimerStop() => TimerStopAction?.Invoke();
        public static void TimerReset() => TimerResetAction?.Invoke();

        //sends the current tick
        public static event Action<int> OnTickEvent;
    
        public static int tick;
        public static float tickTimer;

        void Start()
        {
            GetTimer();
        }

        void Update()
        {
            //add time passed since last update
            tickTimer += Time.deltaTime;

            //if time passed is greater than the length of one tick
            if (tickTimer >= GameManager._GM.TickDuration)
            {
                //increment the tick count
                tick++;


                //reset tickTimer
                tickTimer -= GameManager._GM.TickDuration;

                //if OnTick has subscribers, fire the relevent event
                if (OnTickEvent != null) OnTickEvent(tick);
            }
        }
    }
}