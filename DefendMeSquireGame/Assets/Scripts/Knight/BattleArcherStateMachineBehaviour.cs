using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Naive2DMovementAgent))]
public class BattleArcherStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private GameObject archerEnemy;

    [SerializeField]
    private GameObject brawl;

    [SerializeField]
    private float moveSpeed = 2;

    private SpriteRenderer spriteRenderer;
    private Naive2DMovementAgent movementAgent;

    private bool hasFinishedFight = false;

    public override void OnStateEnter()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementAgent = GetComponent<Naive2DMovementAgent>();
    }

    public override bool OnStateUpdate()
    {
        if (archerEnemy == null)
        {
            Debug.Log("archer probably dead. Ignoring");
        }
        else
        {
            movementAgent.TargetLocation = new Vector2(archerEnemy.transform.position.x, transform.position.y);

            if (movementAgent.ReachedDestination)
            {
                FightArcher();
            }
        }

        return hasFinishedFight;
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

        hasFinishedFight = true;
    }
}
