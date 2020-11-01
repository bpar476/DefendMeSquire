using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCheckpoint : MonoBehaviour
{
    public void DoCheckpoint()
    {
        CheckpointManager.Instance.TriggerThirdFloorCheckpoint();
    }
}
