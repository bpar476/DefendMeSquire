using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTask : MonoBehaviour
{
    public float duration;
    
    private SpriteRenderer myRenderer;

    private LoadingBar loadingBar;

    private bool done;

    private bool isPlayerDoingTask = false;
    private bool playerIsOnTask = false;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        loadingBar = GetComponentInChildren<LoadingBar>();
        myRenderer = GetComponent<SpriteRenderer>();
        if (loadingBar == null || myRenderer == null) {
            throw new System.Exception("Task not set up properly");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (done) {
            return;
        }

        UpdateDoingTask();

        if (isPlayerDoingTask) {
            IncrementTime();

            float completeness = time / duration;

            loadingBar.SetCompleteness(completeness);

            if (completeness >= 1) {
                done = true;
                
                RenderCompletedTask();
            }
        } else {
            time = 0.0f;
            loadingBar.SetCompleteness(0);
        }
    }

    private void UpdateDoingTask() {
        bool playerTryingToDotask = Input.GetAxis("Interaction") != 0;
        if (playerTryingToDotask && playerIsOnTask) {
            isPlayerDoingTask = true;
        } else {
            isPlayerDoingTask = false;
        }
    }

    private void RenderCompletedTask() {
        myRenderer.color = Color.magenta;
    }

    private void IncrementTime()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (IsPlayer(other.gameObject)) {
            playerIsOnTask = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (IsPlayer(other.gameObject)) {
            playerIsOnTask = false;
        }
    }

    private bool IsPlayer(GameObject gobj) {
            return gobj.tag == "Player";
    }
}
