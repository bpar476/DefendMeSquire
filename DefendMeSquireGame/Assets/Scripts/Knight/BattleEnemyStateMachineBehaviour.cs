﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Naive2DMovementAgent))]
public class BattleEnemyStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject brawl;

    [SerializeField]
    private float moveSpeed = 2;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Naive2DMovementAgent movementAgent;

    private bool hasFinishedFight = false;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public override void OnStateEnter()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementAgent = GetComponent<Naive2DMovementAgent>();
    }

    public override bool OnStateUpdate()
    {
        if (enemy != null)
        {
            movementAgent.TargetLocation = new Vector2(enemy.transform.position.x, transform.position.y);

            if (movementAgent.ReachedDestination)
            {
                FightEnemy();
            }
        }

        return hasFinishedFight;
    }

    private void FightEnemy()
    {

        spriteRenderer.enabled = false;
        Destroy(enemy.gameObject);
        audioSource.Play();
        brawl.SetActive(true);

        StartCoroutine(EndBrawl(3f));
    }

    private IEnumerator EndBrawl(float brawlDuration)
    {
        yield return new WaitForSeconds(brawlDuration);

        brawl.SetActive(false);
        audioSource.Stop();
        spriteRenderer.enabled = true;

        hasFinishedFight = true;
    }
}
