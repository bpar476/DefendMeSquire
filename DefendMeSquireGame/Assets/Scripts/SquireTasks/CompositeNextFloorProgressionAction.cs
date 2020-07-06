using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeNextFloorProgressionAction : NextFloorProgressionAction
{

    public NextFloorProgressionAction[] children;

    public override void OnProgressToNextFloor()
    {
        foreach (var action in children)
        {
            action.OnProgressToNextFloor();
        }
    }
}
