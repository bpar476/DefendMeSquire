using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawbridge : AbstractTriggeredEvent
{
    public override void OnTrigger()
    {
        Debug.Log("dropping drawbridge");
        StartCoroutine(LowerDrawbridge());
    }

    private IEnumerator LowerDrawbridge()
    {
        for (float i = 90; i >= 0; i -= 30 * Time.deltaTime)
        {
            transform.rotation = Quaternion.Euler(0, 0, i);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return null;
    }
}
