using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThemeWithMelodyOnStart : MonoBehaviour
{
    void Start()
    {
        MyMusicManager.Instance.PlayMainTheme();
    }
}
