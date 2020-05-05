using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{

    public List<Sprite> loadingSprites;
    private SpriteRenderer myRenderer;
    public float duration;
    private bool finished = false;
    private int currentIncrement = 0;
    private float time = 0.0f;
    private float totalTime = 0.0f;
    private float incrementThreshold
    {
        get
        {
            return duration / increments;
        }
    }
    private int increments
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

    private void Update()
    {
        if (finished)
        {
            return;
        }

        IncrementTime();

        if (HasSurpassedLoadingIncrementDuration())
        {
            currentIncrement++;

            SetLoadingBarSpriteToCurrentIncrement();

            UpdateIfLoadingFinished();

            ResetIncrementTimer();
        }
    }

    private void IncrementTime()
    {
        time += Time.deltaTime;
    }

    private bool HasSurpassedLoadingIncrementDuration()
    {
        return time >= incrementThreshold;
    }

    private void SetLoadingBarSpriteToCurrentIncrement()
    {
        myRenderer.sprite = loadingSprites[currentIncrement];
    }

    private void UpdateIfLoadingFinished()
    {
        if (currentIncrement == increments)
        {
            finished = true;
        }
    }

    private void ResetIncrementTimer() {
        time -= incrementThreshold;
    }
}
