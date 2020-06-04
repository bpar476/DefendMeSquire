using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    private float timer = 0;
    private Dictionary<int, TimerData> listeners = new Dictionary<int, TimerData>();
    private int stopwatchIds = 0;

    class TimerData
    {
        public float period;
        public float lastTick;
        public GlobalTimerStopwatch listener;
        public bool active;

        public TimerData(float offset, float period, float currentTime, GlobalTimerStopwatch listener)
        {
            this.period = period;
            this.lastTick = currentTime + offset;
            this.listener = listener;
            this.active = true;
        }
    }

    /// <summary>
    /// Adds a new listener to the timer. It will be notified every GlobalTimerStopwatch.Period after GlobalTimerStopwatch.Offset has occured
    /// </summary>
    /// <param name="listener">The listener to register with the stopwatch</param>
    /// <returns>An integer ID which can be used to remove the listener from the stopwatch in the future</returns>
    public int AddStopwatch(GlobalTimerStopwatch listener)
    {
        int id = stopwatchIds;
        listeners.Add(id, new TimerData(listener.Offset(), listener.Period(), timer, listener));
        stopwatchIds++;
        return id;
    }

    /// <summary>
    /// Removes the stopwatch with the given id from the global timer
    /// </summary>
    /// <param name="id">ID of the stopwatch to remove</param>
    public void RemoveStopwatch(int id)
    {
        listeners[id].active = false;
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
        foreach (KeyValuePair<int, TimerData> entry in listeners)
        {
            var data = entry.Value;
            if (!data.active)
            {
                continue;
            }
            float difference = timer - data.lastTick;
            if (difference >= data.period)
            {
                //Tick
                data.listener.OnTick();

                data.lastTick = timer - (difference - data.period);

            }
        }
    }
}
