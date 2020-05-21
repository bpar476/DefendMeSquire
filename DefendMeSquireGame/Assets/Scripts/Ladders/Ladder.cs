using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed;

    private GameObject player;
    private bool playerInLadder;
    private bool playerOnLadder;
    private bool playerCanReachLadder;

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (playerInLadder)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            Collider2D collider = GetComponent<Collider2D>();
            if (verticalInput != 0)
            {
                if (verticalInput < 0 || playerCanReachLadder)
                {
                    playerOnLadder = true;
                    playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Sign(verticalInput) * climbSpeed);
                    UpdateCanReachLadder();
                }
            }
            else
            {
                if (playerOnLadder)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                }
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
            player = other.gameObject;

            playerInLadder = true;
            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;

            UpdateCanReachLadder();
        }
    }

    private void UpdateCanReachLadder()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (player.transform.position.y < collider.bounds.max.y)
        {
            playerCanReachLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInLadder = false;
            playerOnLadder = false;
            playerCanReachLadder = false;

            player = other.gameObject;

            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 1;
        }
    }
}
