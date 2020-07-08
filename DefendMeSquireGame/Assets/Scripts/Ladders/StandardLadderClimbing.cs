using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardLadderClimbing : MonoBehaviour, LadderClimbing
{
    public void ClimbLadder(Rigidbody2D rigidbody2D, float climbVelocityYComponent)
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, climbVelocityYComponent);
    }
}
