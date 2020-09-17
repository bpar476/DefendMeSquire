using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFromLocation : MonoBehaviour
{
    [SerializeField]
    private float patrolDistance;
    private Vector3 initialPosition;
    private Vector3 currentTargetPosition;
    private Vector3 patrolTranslation;

    private void Awake()
    {
        initialPosition = transform.position;
        patrolTranslation = new Vector3(patrolDistance, 0, 0);
        currentTargetPosition = initialPosition - patrolTranslation;
    }

    private void Update()
    {
        UpdateTargetLocation();

        MoveTowardsTarget();
    }

    private void UpdateTargetLocation()
    {
        if (Mathf.Abs(transform.position.x - currentTargetPosition.x) < 0.05f)
        {
            var prevTranslation = currentTargetPosition - initialPosition;
            currentTargetPosition = initialPosition - prevTranslation;
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, 0.002f);
    }
}
