using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public float duration;

    public List<Sprite> loadingSprites;
    private bool finished = false;
    private SpriteRenderer myRenderer;
    private int currentIncrement = 0;
    private float time = 0.0f;
    private float totalTime = 0.0f;
    private float incrementThreshold {
        get {
            return duration / increments;
        }
    }
    private int increments {
        get {
            return loadingSprites.Count - 1;
        }
    }

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (finished) {
            return;
        }

        time += Time.deltaTime;
        totalTime += Time.deltaTime;

        if (time >= incrementThreshold) {
            currentIncrement++;
            myRenderer.sprite = loadingSprites[currentIncrement];

            if (currentIncrement == increments) {
                finished = true;
                Debug.Log(totalTime);
            }

            time -= incrementThreshold;
        }
    }
}
