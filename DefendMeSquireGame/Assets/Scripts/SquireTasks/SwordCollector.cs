using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    public SquireTaskCompletionListener[] completionListeners;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollectSword(other.GetComponent<SwordTask>());
    }

    private void CollectSword(SwordTask sword)
    {
        if (sword == null)
        {
            throw new System.Exception("No sword component in SwordCollector receiver. Layer matrix is incorrect");
        }
        sword.GiveSword(transform);
        foreach (var listener in completionListeners)
        {
            listener.onTaskCompleted();
        }
    }
}
