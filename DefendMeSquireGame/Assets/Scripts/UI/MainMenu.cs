using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool hasLoadedLevel = false;

    public void StartGame()
    {
        if (!hasLoadedLevel)
        {
            SceneManager.LoadSceneAsync((int)DefendMeSquireScenes.FirstLevel);
            hasLoadedLevel = true;
        }
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
