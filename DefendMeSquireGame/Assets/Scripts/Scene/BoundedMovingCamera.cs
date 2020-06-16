using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoundedMovingCamera : MonoBehaviour
{

    public float upperBound;
    public float lowerBound;
    private float cameraRadius = 0;
    public Color gizmoColour;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColour;
        Vector3 direction = Vector3.up;
        Gizmos.DrawLine(transform.position, transform.position - direction * cameraRadius);
        Gizmos.DrawLine(transform.position, transform.position + direction * cameraRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            this.enabled = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.transform.position.y + 1.8f, lowerBound, upperBound), transform.position.z);
        }
    }
}
