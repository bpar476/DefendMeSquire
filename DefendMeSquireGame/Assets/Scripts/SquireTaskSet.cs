using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTaskSet : MonoBehaviour, SquireTaskCompletionListener
{

    /// Ordered list of tasks to be completed to satisfy this task set
    public List<SquireTask> tasks;

    public SquireTask taskPrefab;

    private int currentTaskIndex = 0;

    void Start() {
        foreach(SquireTask task in tasks) {
            SetTaskActive(task, false);
        }
        ActivateCurrentTask();
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

    private void ActivateCurrentTask() {
        SquireTask task = tasks[currentTaskIndex];
        task.RegisterListener(this);
        SetTaskActive(task, true);
    }

    private void SetTaskActive(SquireTask task, bool active) {
        task.gameObject.SetActive(active);
    }
}
