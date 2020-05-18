using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed;

    private GameObject player;
    private bool playerInLadder;

    private bool playerOnLadder;

    private float actualClimbSpeed;

    private void Start()
    {
        actualClimbSpeed = climbSpeed / 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0 && playerInLadder)
        {
            playerOnLadder = true;
            player.GetComponent<Rigidbody2D>().MovePosition(new Vector2(player.transform.position.x, player.transform.position.y) + (Mathf.Sign(verticalInput) * new Vector2(0, 1) * climbSpeed));
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
