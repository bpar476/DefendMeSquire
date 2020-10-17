using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naive2DMovementAgent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    public Vector2 TargetLocation;

    private float tolerance = 0.05f;
    private float originalScaleX;

    private void Awake()
    {
        TargetLocation = transform.position;
        originalScaleX = transform.localScale.x;
    }

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
        if (!ReachedDestination)
        {
            var targetLocation = Vector2.MoveTowards(transform.position, TargetLocation, Mathf.Min(moveSpeed * Time.deltaTime, Vector2.Distance(transform.position, TargetLocation)));

            FaceMovingDirection(targetLocation);

            transform.position = targetLocation;
        }
    }

    private void FaceMovingDirection(Vector2 targetLocation)
    {
        var newXScale = originalScaleX;
        if (targetLocation.x < transform.position.x)
        {
            newXScale *= -1;
        }

        if (newXScale != transform.localScale.x)
        {
            transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
        }
    }
}
