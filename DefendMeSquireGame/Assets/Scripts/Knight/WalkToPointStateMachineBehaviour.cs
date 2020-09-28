using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Naive2DMovementAgent))]
public class WalkToPointStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private Vector2 locationToWalkTo;
    [SerializeField]
    private float moveSpeed = 2;

    private Naive2DMovementAgent movementAgent;

    public override void OnStateEnter()
    {
        movementAgent = GetComponent<Naive2DMovementAgent>();
        movementAgent.TargetLocation = locationToWalkTo;
    }

    public override bool OnStateUpdate()
    {
        return movementAgent.ReachedDestination;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(locationToWalkTo, 0.1f);
    }

}
