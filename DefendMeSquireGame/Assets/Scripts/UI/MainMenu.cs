using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject creditsMenu;

    [SerializeField]
    private GameObject mainMenu;

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

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
}
