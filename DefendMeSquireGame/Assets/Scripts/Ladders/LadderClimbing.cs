using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LadderClimbing
{
    /// <summary>
    /// Moves the given rigidbody along a ladder. Should be called by a Ladder script
    /// </summary>
    /// <param name="rigidbody2D">The body to move along the ladder</param>
    /// <param name="climbVelocity">The y component of the desired climbing velocity</param>
    void ClimbLadder(Rigidbody2D rigidbody2D, float climbVelocityYComponent);
}
