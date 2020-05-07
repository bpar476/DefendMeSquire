using System.Collections;
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
            Debug.Log(currentTaskIndex);
            
            if (currentTaskIndex < 0 || spawnPoints == null || currentTaskIndex > spawnPoints.Length) {
                return null;
            }
            return spawnPoints[currentTaskIndex];
        }
    }

    void Start() {
        spawnPoints =  GameObject.FindObjectsOfType<SquireTaskSpawnPoint>();

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
        timer += Time.deltaTime;

        if (timer > currentTaskInterval) {
            timer -= currentTaskInterval;
            ActivateCurrentTask();
        }
    }

    private void ActivateCurrentTask() {
        SquireTask task = Instantiate(taskPrefab, nextTaskSpawn.transform.position, Quaternion.identity);

        task.RegisterListener(this);
    }

    public void onTaskCompleted() {
        DeactivateCurrentTask();

        currentTaskIndex++;

        if (currentTaskIndex == tasks.Count) {
            HandleAllTasksFinished();
        } else {
            ActivateCurrentTask();
        }
    }

    private void DeactivateCurrentTask() {
        SquireTask task = tasks[currentTaskIndex];
    } 

    private void HandleAllTasksFinished() {
        Debug.Log("Completed all tasks!");
    }


    private void SetTaskActive(SquireTask task, bool active) {
        task.gameObject.SetActive(active);
    }
}
