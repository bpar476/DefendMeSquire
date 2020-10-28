using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderToThirdFloor : MonoBehaviour
{
    private void Start()
    {
        if (CheckpointManager.Instance.HasCheckpointed())
            GetComponent<EndOfLevelLadder>().SetEndState();
    }
}
