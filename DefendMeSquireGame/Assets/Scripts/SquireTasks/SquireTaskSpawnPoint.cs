﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTaskSpawnPoint : MonoBehaviour, SquireTaskCompletionListener
{
    public Vector2 spawnLocation;
    public int spawnDelaySeconds;
    public SquireTask taskPrefab;

    public List<GameObject> objectsActiveDuringThisTask;

    private void Start()
    {
        SetActivityOfPairedTasks(false);
    }

    public void onTaskCompleted()
    {
        SetActivityOfPairedTasks(false);
    }

    public SquireTask Spawn()
    {
        SquireTask task = Instantiate(taskPrefab, transform.position, Quaternion.identity);
        task.RegisterListener(this);

        SetActivityOfPairedTasks(true);
        return task;
    }

    private void SetActivityOfPairedTasks(bool active)
    {
        objectsActiveDuringThisTask.ForEach(gobj => gobj.SetActive(active));
    }
}
