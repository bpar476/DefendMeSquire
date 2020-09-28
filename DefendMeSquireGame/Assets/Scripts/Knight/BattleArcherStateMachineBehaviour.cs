using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BattleArcherStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private GameObject archerEnemy;

    [SerializeField]
    private GameObject brawl;

    [SerializeField]
    private float moveSpeed = 2;

    private SpriteRenderer spriteRenderer;

    private bool hasFinishedFight = false;

    public override void OnStateEnter()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override bool OnStateUpdate()
    {
        if (archerEnemy == null)
        {
            Debug.Log("archer probably dead. Ignoring");
        }
        else
        {
            // TODO refactor this to re-use WalkToPointStateMachineBehaviour
            var currentDestination = new Vector2(archerEnemy.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, currentDestination, moveSpeed * Time.deltaTime);

            if (transform.position.Equals(currentDestination))
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
