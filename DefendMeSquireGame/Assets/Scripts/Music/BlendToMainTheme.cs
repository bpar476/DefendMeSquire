using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendToMainTheme : MonoBehaviour
{
    public void DoBlendToMainTheme()
    {
        MyMusicManager.Instance.BlendIntoMainTheme();
    }
}
