using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeCameraTransition : MonoBehaviour
{
    public float newUpperBound;
    public float newLowerBound;

    public void UpdateCameraBounds()
    {
        var cam = Camera.main.GetComponent<BoundedMovingCamera>();
        cam.upperBound = newUpperBound;
        cam.lowerBound = newLowerBound;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            UpdateCameraBounds();
        }
    }
}
