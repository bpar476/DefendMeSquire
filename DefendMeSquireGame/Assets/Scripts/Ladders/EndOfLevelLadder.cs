﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelLadder : NextFloorProgressionAction
{
    public GameObject ladder;
    public GameObject ladderCover;

    private float ladderDropHeight = 3.0f;
    private float coverRotation = 100.0f;
    private float coverRotateDuration = 0.4f;
    private float ladderDropDuration = 2f;

    public override void onProgressToNextFloor()
    {
        StartCoroutine(RevealLadder());
    }
    public IEnumerator RevealLadder()
    {
        float initialZRotation = ladderCover.transform.eulerAngles.z;
        float rotRate = coverRotation / coverRotateDuration;
        float interval = 0.01f;
        float delta = rotRate * interval;
        for (float rot = 0f; rot <= coverRotation; rot += delta)
        {
            ladderCover.transform.eulerAngles = new Vector3(ladderCover.transform.eulerAngles.x, ladderCover.transform.eulerAngles.y, initialZRotation - rot);
            yield return new WaitForSeconds(interval);
        }

        float initialYPos = ladder.transform.position.y;
        float dropRate = ladderDropHeight / ladderDropDuration;
        float ladderDelta = dropRate * interval;
        for (float len = 0f; len <= ladderDropHeight; len += ladderDelta)
        {
            ladder.transform.position = new Vector3(transform.position.x, initialYPos - len, transform.position.z);
            yield return new WaitForSeconds(interval);
        }
    }
}
