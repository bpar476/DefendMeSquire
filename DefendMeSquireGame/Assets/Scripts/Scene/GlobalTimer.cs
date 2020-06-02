using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    private float timer = 0;
    private bool running = false;
    private List<TimerData> listeners = new List<TimerData>();

    class TimerData
    {
        public float period;
        public float lastTick;
        public int id;
        public GlobalTimerStopwatch listener;

        public TimerData(float offset, float period, float currentTime, int id, GlobalTimerStopwatch listener)
        {
            this.period = period;
            this.lastTick = currentTime + offset;
            this.listener = listener;
        }
    }

    public void AddStopwatch(GlobalTimerStopwatch listener)
    {
        listeners.Add(new TimerData(listener.Offset(), listener.Period(), timer, listeners.Count, listener));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTimer();

        CallListeners();
    }

    private void UpdateTimer()
    {
        timer += Time.fixedDeltaTime;
    }

    private void CallListeners()
    {
        listeners.ForEach((data) =>
        {
            float difference = timer - data.lastTick;
            if (difference >= data.period)
            {
                //Tick
                data.listener.OnTick();

                data.lastTick = timer - (difference - data.period);
            }
        });
    }
}
