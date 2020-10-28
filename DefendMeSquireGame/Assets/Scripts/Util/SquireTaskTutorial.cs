using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTaskTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorial;
    [SerializeField]
    private bool disableOnAwake = true;
    // Start is called before the first frame update
    private void Awake()
    {
        tutorial.SetActive(!disableOnAwake);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && disableOnAwake)
        {
            tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && disableOnAwake)
        {
            tutorial.SetActive(false);
        }
    }

    public void HideTutorial()
    {
        tutorial.SetActive(false);
    }
}
