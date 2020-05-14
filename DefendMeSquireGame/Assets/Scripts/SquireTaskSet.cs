﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTaskSet : MonoBehaviour, SquireTaskCompletionListener
{

    /// Ordered list of tasks to be completed to satisfy this task set
    public List<SquireTask> tasks;

    public SquireTask taskPrefab;


    private float timer = 0;
    private float currentTaskInterval;

    private int currentTaskIndex = -1;
    private SquireTaskSpawnPoint[] spawnPoints;
    private SquireTaskSpawnPoint nextTaskSpawn {
        get {            
            if (currentTaskIndex < 0 || spawnPoints == null || currentTaskIndex > spawnPoints.Length) {
                return null;
            }
            return spawnPoints[currentTaskIndex];
        }
    }
    private bool readyToSpawnTask = true;

    void Start() {
        spawnPoints = GameObject.FindObjectsOfType<SquireTaskSpawnPoint>();

        System.Array.Sort(spawnPoints, (x, y) => {
            return x.spawnDelaySeconds - y.spawnDelaySeconds;
        });

        QueueNextTaskSpawn();
    }

    private void QueueNextTaskSpawn() {
        currentTaskIndex++;
        currentTaskInterval = nextTaskSpawn.spawnDelaySeconds;
    }

    private void Update() {
        if (readyToSpawnTask) {
            timer += Time.deltaTime;

            if (timer > currentTaskInterval) {
                timer -= currentTaskInterval;
                ActivateCurrentTask();
            }
        }
    }

    private void ActivateCurrentTask() {
        SquireTask task = Instantiate(taskPrefab, nextTaskSpawn.transform.position, Quaternion.identity);

        task.RegisterListener(this);

        readyToSpawnTask = false;
    }

    public void onTaskCompleted() {
        if (currentTaskIndex == spawnPoints.Length - 1) {
            HandleAllTasksFinished();
        } else {
            QueueNextTaskSpawn();
            readyToSpawnTask = true;
        }
    }

    private void HandleAllTasksFinished() {
        Debug.Log("Completed all tasks!");
    }
}