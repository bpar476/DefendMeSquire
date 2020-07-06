using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObjectNextFloorAction : NextFloorProgressionAction
{

    public GameObject gobj;

    public override void OnProgressToNextFloor()
    {
        gobj.SetActive(true);
    }

}
