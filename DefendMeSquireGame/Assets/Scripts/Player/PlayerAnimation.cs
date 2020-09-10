using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerAnimation : MonoBehaviour, MovementListener, DeathListener
{

    private static readonly string TRIGGER_DEATH = "die";
    private static readonly string FLOAT_SPEED = "speed";
    private static readonly string BOOL_CLIMBING = "climbing";
    private static readonly string BOOL_ON_LADDER = "onLadder";

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool turnedAround = false;
    private Vector3 originalLocalScale;
    private Vector3 turnedAroundLocalScale;
    private bool tryingToClimbLadder = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        var localScale = transform.localScale;
        originalLocalScale = new Vector3(localScale.x, localScale.y, localScale.z);
        turnedAroundLocalScale = new Vector3(-localScale.x, localScale.y, localScale.z);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb2d.velocity.y) > 0.1 && tryingToClimbLadder)
        {
            animator.SetBool(BOOL_CLIMBING, true);
        }
        else
        {
            animator.SetBool(BOOL_CLIMBING, false);
        }
    }

    public void OnDeath()
    {
        animator.SetTrigger(TRIGGER_DEATH);
    }

    public void OnMove(Vector2 movement)
    {
        float xSpeed = movement.x;
        if (xSpeed < 0)
        {
            transform.localScale = turnedAroundLocalScale;
            turnedAround = true;
        }
        else if (turnedAround && xSpeed > 0)
        {
            transform.localScale = originalLocalScale;
        }
        animator.SetFloat(FLOAT_SPEED, Mathf.Abs(xSpeed));
    }

    public void OnLadderMount()
    {
        animator.SetBool(BOOL_ON_LADDER, true);
    }

    public void OnLadderClimb()
    {
        tryingToClimbLadder = true;
    }

    public void OnLadderHalt()
    {
        tryingToClimbLadder = false;
    }

    public void OnLadderDismount()
    {
        animator.SetBool(BOOL_ON_LADDER, false);
    }


}
