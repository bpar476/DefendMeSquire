using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseForSecondsStateMachineBehaviour : MyStateMachineBehaviour
{

    [SerializeField]
    private float waitSeconds = 2.5f;

    private float enterTime;

    public override void OnStateEnter()
    {
        enterTime = Time.time;
    }

    public override bool OnStateUpdate()
    {
        return Time.time - enterTime >= waitSeconds;
    }
}
