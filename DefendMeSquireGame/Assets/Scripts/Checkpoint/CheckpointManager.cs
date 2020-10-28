using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a very bad way of implementing checkpoints and is heavily coupled to the only scene in this project.
/// Do not use this as a reference implementation.
/// </summary>
public class CheckpointManager : MonoBehaviour
{

    public static CheckpointManager Instance { get; private set; }

    private GameObject player;
    private EndOfLevelLadder ladderToThirdFloor;
    private GameObject firstLevel;
    private BoundedMovingCamera mainCamera;

    private bool hasReachedThirdFloor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            hasReachedThirdFloor = false;
            DontDestroyOnLoad(this);
        }
        else
        {
            GameObject.Destroy(this);
        }
    }

    public void TriggerThirdFloorCheckpoint()
    {
        hasReachedThirdFloor = true;
    }

    public bool HasCheckpointed()
    {
        return hasReachedThirdFloor;
    }
}
