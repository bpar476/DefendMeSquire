using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed;

    private GameObject player;
    private bool playerInLadder;

    private bool playerOnLadder;

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (playerInLadder)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (verticalInput != 0)
            {
                playerOnLadder = true;
                playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Sign(verticalInput) * climbSpeed);
                player.GetComponent<Movement2D>().enabled = false;
            }
            else
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                player.GetComponent<Movement2D>().enabled = true;
            }
        }
    }

    public bool IsPlayerOnLadder()
    {
        return playerOnLadder;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInLadder = true;

            player = other.gameObject;

            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInLadder = false;
            playerOnLadder = false;

            player = other.gameObject;

            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 1;
        }
    }
}
