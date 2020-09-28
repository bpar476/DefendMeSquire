using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseShieldStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private GameObject shield;

    public override void OnStateEnter()
    {
        shield.SetActive(true);
    }

    public override bool OnStateUpdate()
    {
        // Listen for when crank has been fully turned
        return false;
    }
}
