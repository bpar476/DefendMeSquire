using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseShieldStateMachineBehaviour : MyStateMachineBehaviour
{
    private readonly string ANIM_TRIGGER_SHIELD = "shield";

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private AbstractTrigger trigger;

    private Animator animator;

    // TODO refactor this to use the TriggeredEventStateMachineBehaviour
    private bool hasFinished;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        trigger.OnTrigger += () =>
        {
            hasFinished = true;
        };
    }

    public override void OnStateEnter()
    {
        shield.SetActive(true);
        animator.SetTrigger(ANIM_TRIGGER_SHIELD);
    }

    public override bool OnStateUpdate()
    {
        return hasFinished;
    }
}
