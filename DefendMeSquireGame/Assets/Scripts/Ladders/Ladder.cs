using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LadderClimbing))]
public class Ladder : MonoBehaviour
{
    public float climbSpeed;
    private GameObject player;
    private PlayerAnimation playerAnimation;
    private bool playerInLadder;
    private bool playerOnLadder;
    private bool playerCanReachLadder;
    private bool playerIsClimbing;

    private LadderClimbing climbing;

    private void Awake()
    {
        climbing = GetComponent<LadderClimbing>();
    }

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
                    if (!playerOnLadder)
                    {
                        playerOnLadder = true;
                        playerAnimation.OnLadderMount();
                    }

                    if (!playerIsClimbing)
                    {
                        playerIsClimbing = true;
                        playerAnimation.OnLadderClimb();
                    }

                    climbing.ClimbLadder(playerRb, Mathf.Sign(verticalInput) * climbSpeed);
                    UpdateCanReachLadder();
                }
            }
            else
            {
                if (playerOnLadder)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                    playerIsClimbing = false;
                    playerAnimation.OnLadderHalt();
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
            playerAnimation = player.GetComponent<PlayerAnimation>();
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
            player = other.gameObject;
            playerAnimation = player.GetComponent<PlayerAnimation>();
            playerInLadder = false;
            playerOnLadder = false;
            playerCanReachLadder = false;
            playerIsClimbing = false;
            playerAnimation.OnLadderDismount();


            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 1;
        }
    }
}
