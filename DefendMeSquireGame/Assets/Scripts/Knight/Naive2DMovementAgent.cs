using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naive2DMovementAgent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    public Vector2 TargetLocation;

    private float tolerance = 0.05f;

    public bool ReachedDestination
    {
        get
        {
            return Vector2.Distance(transform.position, TargetLocation) < tolerance;
        }
    }

    private void Update()
    {
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetLocation, Mathf.Min(moveSpeed * Time.deltaTime, Vector2.Distance(transform.position, TargetLocation)));
    }
}
