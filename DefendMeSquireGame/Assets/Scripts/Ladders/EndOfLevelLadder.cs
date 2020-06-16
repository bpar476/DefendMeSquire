using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelLadder : NextFloorProgressionAction
{
    public GameObject ladder;
    public GameObject ladderCover;

    public override void onProgressToNextFloor()
    {
        RevealLadder();
    }
    public void RevealLadder()
    {
        OpenLadderCover();

        ExtendLadder();
    }
    private void ExtendLadder()
    {
        ladder.transform.position -= new Vector3(0, 3, 0);
    }

    private void OpenLadderCover()
    {
        ladderCover.transform.Rotate(0, 0, -90);
    }
}
