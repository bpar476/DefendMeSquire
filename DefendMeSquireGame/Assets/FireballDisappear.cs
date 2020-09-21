using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDisappear : StateMachineBehaviour
{

    private Fireball fireball;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        fireball = animator.gameObject.GetComponent<Fireball>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireball.OnExplosionFinished();
    }
}
