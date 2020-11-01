using UnityEngine;
using Fungus;

public class DisableFullCutsceneIfCheckpointed : MonoBehaviour
{
    private void Start()
    {
        if (CheckpointManager.Instance.HasCheckpointed())
        {
            GetComponent<Flowchart>().SetBooleanVariable("HasCheckpoint", true);
        }
    }
}
