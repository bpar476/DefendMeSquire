using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTaskSpawnPoint : MonoBehaviour, SquireTaskCompletionListener
{
    public Vector2 spawnLocation;
    public int spawnDelaySeconds;
    public SquireTask taskPrefab;

    public List<GameObject> objectsActiveDuringThisTask;

    public void onTaskCompleted()
    {
        objectsActiveDuringThisTask.ForEach(gobj => gobj.SetActive(false));
    }

    public SquireTask Spawn()
    {
        SquireTask task = Instantiate(taskPrefab, transform.position, Quaternion.identity);
        task.RegisterListener(this);

        objectsActiveDuringThisTask.ForEach(gobj => gobj.SetActive(true));
        return task;
    }
}
