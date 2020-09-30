using UnityEngine;

public class TriggeredEventStateMachineBehaviour : MyStateMachineBehaviour
{
    [SerializeField]
    private AbstractTrigger trigger;

    private bool hasBeenTriggered = false;

    private void Start()
    {
        trigger.OnTrigger += () => hasBeenTriggered = true;
    }

    public override bool OnStateUpdate()
    {
        return hasBeenTriggered;
    }
}
