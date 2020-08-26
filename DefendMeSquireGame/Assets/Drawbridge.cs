using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawbridge : AbstractTriggeredEvent
{
    public override void OnTrigger()
    {
        Debug.Log("dropping drawbridge");
    }
}
