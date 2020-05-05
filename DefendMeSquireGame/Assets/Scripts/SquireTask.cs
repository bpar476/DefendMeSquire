using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTask : MonoBehaviour
{

    private LoadingBar loadingBar;

    private bool isBeingCompleted = false;
    private bool playerIsOnTask = false;

    // Start is called before the first frame update
    void Start()
    {
        loadingBar = GetComponentInChildren<LoadingBar>();
        if (loadingBar == null) {
            throw new System.Exception("loading bar is not on child component");
        }
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

    // Update is called once per frame
    void Update()
    {
        bool playerTryingToDotask = Input.GetAxis("Interaction") != 0;
        if (playerTryingToDotask && playerIsOnTask) {
            isBeingCompleted = true;
            StartPerformingTask();
        } else {
            if (isBeingCompleted) {
                StopPerformingTask();
            }
            isBeingCompleted = false;
        }
    }

    void StartPerformingTask() {
        Debug.Log("Player is doing task");
        // Start loading bar
    }

    void StopPerformingTask() {
        Debug.Log("Player stopped doing task");
        // Stop loading bar
    }

}
