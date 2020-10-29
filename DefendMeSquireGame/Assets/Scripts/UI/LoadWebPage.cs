using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWebPage : MonoBehaviour
{
    [SerializeField]
    private string URL;

    public void OpenWebPage()
    {
        Application.OpenURL(URL);
    }
}
