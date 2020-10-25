using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TriggerFlowchartStateMachineBehaviour : MyStateMachineBehaviour
{

    /// <summary>
    /// The fungus flowchart containing the block to execute
    /// </summary>
    [SerializeField]
    private Flowchart flowchartToTrigger;

    /// <summary>
    /// The name of the block on the flowchart to execute
    /// </summary>
    [SerializeField]
    private string flowchartBlock;

    private bool hasTriggered;

    public override void OnStateEnter()
    {
        Debug.Log("triggering block");
        hasTriggered = true;
        flowchartToTrigger.ExecuteBlock(flowchartBlock);
    }

    public override bool OnStateUpdate()
    {
        return hasTriggered;
    }
}
