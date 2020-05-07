using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTask : MonoBehaviour
{
    public float duration;

    private SpriteRenderer myRenderer;

    private LoadingBar loadingBar;
    private float completeness;

    private bool done;

    private bool isPlayerDoingTask = false;
    private bool playerIsOnTask = false;

    private float time;

    private List<SquireTaskCompletionListener> listeners = new List<SquireTaskCompletionListener>();

    // Start is called before the first frame update
    void Start()
    {
        loadingBar = GetComponentInChildren<LoadingBar>();
        myRenderer = GetComponent<SpriteRenderer>();
        if (loadingBar == null || myRenderer == null)
        {
            throw new System.Exception("Task not set up properly");
        }
        loadingBar.gameObject.SetActive(false);
    }

    public void RegisterListener(SquireTaskCompletionListener listener) {
        listeners.Add(listener);
    }

    public void DeRegisterListener(SquireTaskCompletionListener listener) {
        listeners.Remove(listener);
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            return;
        }

        UpdateDoingTask();

        if (isPlayerDoingTask)
        {
            UpdateTaskProgress();

            HandleIfTaskIsComplete();
        }
        else
        {
            ResetProgress();
        }
    }

    private void UpdateDoingTask()
    {
        bool playerTryingToDotask = Input.GetAxis("Interaction") != 0;
        if (playerTryingToDotask && playerIsOnTask)
        {
            StartTaskCompletion();
        }
        else if (isPlayerDoingTask)
        {
            StopTaskCompletion();
        }
    }
    
    private void StartTaskCompletion() {
        isPlayerDoingTask = true;
        loadingBar.gameObject.SetActive(true);
    }

    private void StopTaskCompletion() {
        isPlayerDoingTask = false;
        loadingBar.gameObject.SetActive(false);
    }

    private void UpdateTaskProgress()
    {
        IncrementTime();

        completeness = time / duration;

        loadingBar.SetCompleteness(completeness);
    }

    private void HandleIfTaskIsComplete()
    {
        if (completeness >= 1)
        {
            done = true;

            RenderCompletedTask();

            StopTaskCompletion();

            listeners.ForEach(listener => listener.onTaskCompleted());
        }
    }

    private void ResetProgress() {
        time = 0;
        loadingBar.SetCompleteness(0);
        completeness = 0;
    }

    private void RenderCompletedTask()
    {
        myRenderer.color = Color.magenta;
    }

    private void IncrementTime()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other.gameObject))
        {
            playerIsOnTask = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other.gameObject))
        {
            playerIsOnTask = false;
        }
    }

    private bool IsPlayer(GameObject gobj)
    {
        return gobj.tag == "Player";
    }
}
