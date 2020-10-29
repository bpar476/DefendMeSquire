using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameMenu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene((int)DefendMeSquireScenes.MainMenu);
    }
}
