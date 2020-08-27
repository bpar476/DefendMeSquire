using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTask : MonoBehaviour
{
    private bool isPlayerOnSword = false;
    private GameObject player = null;
    private bool isPlayerHoldingSword = false;

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
        if (!isPlayerHoldingSword && Input.GetKeyDown(KeyCode.E))
        {
            PickUpSword();
        }
    }



    private void PickUpSword()
    {
        isPlayerHoldingSword = true;
        transform.SetParent(player.transform);
        player.GetComponent<Movement2D>().maxMoveSpeed = 1;
    }
}
