using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArrow : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var rb2d = animator.GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.velocity = animator.GetComponent<Arrow>().launchVelocity;
    }
}
