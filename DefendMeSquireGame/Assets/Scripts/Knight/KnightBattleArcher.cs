using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBattleArcher : MonoBehaviour
{
    [SerializeField]
    private Vector2[] movementPoints;
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private GameObject archerEnemy;

    private int currentDestinationIndex = 0;
    private Vector2 currentDestination;

    // Update is called once per frame
    void Update()
    {
        if (currentDestinationIndex >= movementPoints.Length)
        {
            currentDestination = new Vector2(archerEnemy.transform.position.x, transform.position.y);
            MoveTowardsDestination();

            if (HasReachedDestination())
            {
                FightArcher();
            }
        }
        else
        {
            currentDestination = movementPoints[currentDestinationIndex];
            MoveTowardsDestination();

            if (HasReachedDestination())
            {
                currentDestinationIndex++;
            }
        }
    }

    private void MoveTowardsDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentDestination, moveSpeed * Time.deltaTime);
    }

    private bool HasReachedDestination()
    {
        return transform.position.Equals(currentDestination);
    }

    private void FightArcher()
    {
        Debug.Log("fighting archer");
    }
}
