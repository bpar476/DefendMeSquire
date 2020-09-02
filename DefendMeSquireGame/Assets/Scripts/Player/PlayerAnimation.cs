﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour, MovementListener, DeathListener
{

    private static readonly string TRIGGER_DEATH = "die";
    private static readonly string FLOAT_SPEED = "speed";

    private Animator animator;
    private bool turnedAround = false;
    private Vector3 originalLocalScale;
    private Vector3 turnedAroundLocalScale;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        var localScale = transform.localScale;
        originalLocalScale = new Vector3(localScale.x, localScale.y, localScale.z);
        turnedAroundLocalScale = new Vector3(-localScale.x, localScale.y, localScale.z);
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
}