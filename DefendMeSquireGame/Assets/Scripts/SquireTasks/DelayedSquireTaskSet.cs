using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSquireTaskSet : MonoBehaviour, SquireTaskCompletionListener, GlobalTimerStopwatch
{
    public NextFloorProgressionAction progressionAction;
    private GlobalTimer timer;
    private int timerId;
    private float currentTaskInterval;

    private int currentTaskIndex = -1;
    private SquireTaskSpawnPoint[] spawnPoints;
    private SquireTaskSpawnPoint nextTaskSpawn
    {
        get
        {
            if (currentTaskIndex < 0 || spawnPoints == null || currentTaskIndex > spawnPoints.Length)
            {
                return null;
            }
            return spawnPoints[currentTaskIndex];
        }
    }

    void Start()
    {
        spawnPoints = GameObject.FindObjectsOfType<SquireTaskSpawnPoint>();

        System.Array.Sort(spawnPoints, (x, y) =>
        {
            return x.spawnDelaySeconds - y.spawnDelaySeconds;
        });

        QueueNextTaskSpawn();
    }

    private void QueueNextTaskSpawn()
    {
        currentTaskIndex++;
        currentTaskInterval = nextTaskSpawn.spawnDelaySeconds;
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
        if (currentTaskIndex == spawnPoints.Length - 1)
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
        progressionAction.onProgressToNextFloor();
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
