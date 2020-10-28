using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (CheckpointManager.Instance.HasCheckpointed())
            gameObject.SetActive(false);
    }
}
