using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThemeNoMelodyOnStart : MonoBehaviour
{
    private void Start()
    {
        MyMusicManager.Instance.PlayThemeNoMelody();
    }
}
