using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationTriggerBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    /// <summary>
    /// The name of the trigger on the given Animator Controller to trigger
    /// </summary>
    private string triggerName;

    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        animator.SetTrigger(triggerName);
    }

    public override bool OnStateUpdate()
    {
        return true;
    }
}
