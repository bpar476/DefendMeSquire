using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToPointStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private Vector2 locationToWalkTo;
    [SerializeField]
    private float moveSpeed = 2;

    public override bool OnStateUpdate()
    {
        MoveTowardsDestination();

        return HasReachedDestination();
    }

    private void MoveTowardsDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, locationToWalkTo, moveSpeed * Time.deltaTime);
    }

    private bool HasReachedDestination()
    {
        return transform.position.Equals(locationToWalkTo);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(locationToWalkTo, 0.1f);
    }

}
