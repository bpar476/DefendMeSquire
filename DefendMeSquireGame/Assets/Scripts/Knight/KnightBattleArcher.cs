using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KnightBattleArcher : MonoBehaviour
{
    [SerializeField]
    private Vector2[] movementPoints;
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private GameObject archerEnemy;

    [SerializeField]
    private GameObject brawl;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private int currentDestinationIndex = 0;
    private Vector2 currentDestination;

    // Update is called once per frame
    void Update()
    {
        if (currentDestinationIndex >= movementPoints.Length)
        {
            // TODO refactor to state machine so this never happens
            if (archerEnemy == null)
            {
                Debug.Log("archer probably dead. Ignoring");
            }
            else
            {
                currentDestination = new Vector2(archerEnemy.transform.position.x, transform.position.y);
                MoveTowardsDestination();

                if (HasReachedDestination())
                {
                    FightArcher();
                }
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
        spriteRenderer.enabled = false;
        Destroy(archerEnemy.gameObject);
        brawl.SetActive(true);

        StartCoroutine(EndBrawl(3f));
    }

    private IEnumerator EndBrawl(float brawlDuration)
    {
        yield return new WaitForSeconds(brawlDuration);

        brawl.SetActive(false);
        spriteRenderer.enabled = true;
    }
}
