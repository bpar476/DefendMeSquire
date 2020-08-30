using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTask : MonoBehaviour
{
    private bool isPlayerOnSword = false;
    private GameObject player = null;
    private bool isPlayerHoldingSword = false;
    private float originalPlayerMoveSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerOnSword = true;
        player = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerOnSword = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerHoldingSword && isPlayerOnSword && Input.GetKeyDown(KeyCode.E))
        {
            PickUpSword();
        }
    }

    private void PickUpSword()
    {
        isPlayerHoldingSword = true;
        transform.SetParent(player.transform);

        Movement2D playerMovement = player.GetComponent<Movement2D>();
        originalPlayerMoveSpeed = playerMovement.maxMoveSpeed;
        playerMovement.maxMoveSpeed = 1;
    }

    public void GiveSword(Transform receiver)
    {
        transform.SetParent(receiver);
        player.GetComponent<Movement2D>().maxMoveSpeed = originalPlayerMoveSpeed;
    }
}
