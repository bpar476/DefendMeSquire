using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyMusicManager : MonoBehaviour
{
    public static MyMusicManager Instance { get; private set; }

    [SerializeField]
    private AudioSource mainTheme;

    [SerializeField]
    private AudioSource themeNoMelody;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void PlayMainTheme()
    {
        if (!mainTheme.isPlaying)
        {
            themeNoMelody.Stop();
            mainTheme.Play();
        }
    }

    public void PlayThemeNoMelody()
    {
        if (!themeNoMelody.isPlaying)
        {
            mainTheme.Stop();
            themeNoMelody.Play();
        }
    }

    public void BlendIntoThemeWithoutMelody()
    {
        if (!themeNoMelody.isPlaying)
        {
            themeNoMelody.Play();
            themeNoMelody.time = mainTheme.time;
            mainTheme.Stop();
        }
    }

    public void BlendIntoMainTheme()
    {
        if (!mainTheme.isPlaying)
        {
            mainTheme.Play();
            mainTheme.time = themeNoMelody.time;
            themeNoMelody.Stop();
        }
    }
}
