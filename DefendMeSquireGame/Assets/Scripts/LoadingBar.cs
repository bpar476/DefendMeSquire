using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{

    public List<Sprite> loadingSprites;
    private SpriteRenderer myRenderer;
    private int currentIncrement = 0;
    private int numIncrements
    {
        get
        {
            return loadingSprites.Count - 1;
        }
    }

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCompleteness(float completeness) {
        int increment = (int) Mathf.Floor(completeness * numIncrements);
        if (increment > numIncrements) {
            increment = numIncrements;
        }

        currentIncrement = increment;
    }


    private void Update()
    { 
        SetLoadingBarSpriteToCurrentIncrement();
    }

    private void SetLoadingBarSpriteToCurrentIncrement()
    {
        myRenderer.sprite = loadingSprites[currentIncrement];
    }
}
