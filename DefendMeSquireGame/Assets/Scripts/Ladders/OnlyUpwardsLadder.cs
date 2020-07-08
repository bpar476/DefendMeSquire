using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyUpwardsLadder : MonoBehaviour, LadderClimbing
{
    public void ClimbLadder(Rigidbody2D rigidbody2D, float climbVelocityYComponent)
    {
        if (climbVelocityYComponent < 0) return;

        rigidbody2D.velocity = new Vector2(0, climbVelocityYComponent);
    }
}
