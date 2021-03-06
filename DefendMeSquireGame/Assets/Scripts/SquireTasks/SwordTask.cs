﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTask : MonoBehaviour
{
    [SerializeField]
    private float playerMovementWhileCarrying = 1;
    private bool isPlayerOnSword = false;
    private GameObject player = null;
    private bool isPlayerHoldingSword = false;
    private float originalPlayerMoveSpeed;

    private SquireTaskTutorial tutorial;

    private void Awake()
    {
        tutorial = GetComponent<SquireTaskTutorial>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only the player and the sword collector can collide with this so we still
        // need to check the tag
        if (other.tag == "Player")
        {
            isPlayerOnSword = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerOnSword = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerHoldingSword && isPlayerOnSword && Input.GetKeyDown(KeyCode.E))
        {
            tutorial.HideTutorial();
            PickUpSword();
        }
    }

    private void PickUpSword()
    {
        isPlayerHoldingSword = true;
        transform.SetParent(player.transform);

        Movement2D playerMovement = player.GetComponent<Movement2D>();
        originalPlayerMoveSpeed = playerMovement.maxMoveSpeed;
        playerMovement.maxMoveSpeed = playerMovementWhileCarrying;

        player.GetComponent<PlayerAnimation>().OnPickupSword();
    }

    public void GiveSword(Transform receiver)
    {
        transform.SetParent(receiver);
        player.GetComponent<Movement2D>().maxMoveSpeed = originalPlayerMoveSpeed;

        player.GetComponent<PlayerAnimation>().OnDropSword();
    }
}
