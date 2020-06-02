using UnityEngine;

/// <summary>
/// Script for helping with timing of actions.
/// Time is updated during FixedUpdate.
/// </summary>
public class Timer : MonoBehaviour
{

    public float period;
    public float offset;

    private float timer;


    private bool running = false;
    private bool tick = false;

    private void Start()
    {
        timer = -offset;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (running)
        {
            UpdateTimer();

            UpdateTicked();
        }
    }

    /// Starts this timer
    public void StartTimer()
    {
        running = true;
    }

    /// Returns true if the timer has ticked this cycle, otherwise returns false
    public bool Ticked()
    {
        return tick;
    }

    private void UpdateTimer()
    {
        timer += Time.fixedDeltaTime;
    }

    private void UpdateTicked()
    {
        if (timer > period)
        {
            timer -= period;

            tick = true;
        }
        tick = false;
    }

}
