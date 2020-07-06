using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSquireTaskSet : MonoBehaviour, SquireTaskCompletionListener, GlobalTimerStopwatch
{
    public NextFloorProgressionAction progressionAction;
    public SquireTaskSpawnPoint[] squireTasks;

    private GlobalTimer timer;
    private int timerId;
    private float currentTaskInterval;

    private int currentTaskIndex = -1;
    private SquireTaskSpawnPoint nextTaskSpawn
    {
        get
        {
            if (currentTaskIndex < 0 || squireTasks == null || currentTaskIndex > squireTasks.Length)
            {
                return null;
            }
            return squireTasks[currentTaskIndex];
        }
    }

    void Start()
    {
        System.Array.Sort(squireTasks, (x, y) =>
        {
            return x.spawnDelaySeconds - y.spawnDelaySeconds;
        });

        QueueNextTaskSpawn();
    }

    private void QueueNextTaskSpawn()
    {
        currentTaskIndex++;
        currentTaskInterval = nextTaskSpawn.spawnDelaySeconds;
        // TODO shouldn't be calling this in Start method
        timerId = GetTimer().AddStopwatch(this);
    }

    private void ActivateCurrentTask()
    {
        SquireTask task = nextTaskSpawn.Spawn();
        task.RegisterListener(this);
        GetTimer().RemoveStopwatch(timerId);
    }

    public void onTaskCompleted()
    {
        if (currentTaskIndex == squireTasks.Length - 1)
        {
            HandleAllTasksFinished();
        }
        else
        {
            QueueNextTaskSpawn();
        }
    }

    private void HandleAllTasksFinished()
    {
        progressionAction.OnProgressToNextFloor();
    }

    public void OnTick()
    {
        ActivateCurrentTask();
    }

    public float Period()
    {
        return currentTaskInterval;
    }

    public float Offset()
    {
        return 0;
    }

    private GlobalTimer GetTimer()
    {
        // if (timer == null)
        {
            timer = FindObjectOfType<GlobalTimer>();
        }
        return timer;
    }
}
