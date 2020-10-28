using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNotification : MonoBehaviour
{

    private TMPro.TMP_Text text;
    private float originalR;
    private float originalG;
    private float originalB;

    private float originalX;
    private float originalZ;

    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
        originalR = text.color.r;
        originalG = text.color.g;
        originalB = text.color.b;

        originalX = text.rectTransform.localPosition.x;
        originalZ = text.rectTransform.localPosition.z;

        Debug.Log(originalR);
        Debug.Log(originalG);

        SetTextAlpha(0);
    }

    public void ShowNotification()
    {
        text.enabled = true;

        StartCoroutine(FadeInNotification());
        StartCoroutine(PopUpNotification());
        StartCoroutine(DismissNotification());
    }

    private IEnumerator FadeInNotification()
    {
        for (float i = 0; i < 256; i += 0.1f)
        {
            SetTextAlpha(i);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private IEnumerator PopUpNotification()
    {
        for (float i = -0.5f; i < 0.5f; i += 0.05f)
        {
            SetTextHeight(i);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator DismissNotification()
    {
        yield return new WaitForSeconds(2f);

        text.enabled = false;

        yield return null;
    }

    private void SetTextAlpha(float alpha)
    {
        text.color = new Color(originalR, originalG, originalB, alpha);
    }

    private void SetTextHeight(float height)
    {
        text.rectTransform.localPosition = new Vector3(originalX, height, originalZ);
    }
}
