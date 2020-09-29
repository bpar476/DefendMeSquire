using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseShieldStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private AbstractTrigger trigger;

    // TODO refactor this to use the TriggeredEventStateMachineBehaviour
    private bool hasFinished;

    private void Awake()
    {
        trigger.OnTrigger += () =>
        {
            hasFinished = true;
        };
    }

    public override void OnStateEnter()
    {
        shield.SetActive(true);
    }

    public override bool OnStateUpdate()
    {
        return hasFinished;
    }
}
