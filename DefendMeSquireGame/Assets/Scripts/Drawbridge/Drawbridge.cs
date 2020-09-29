using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawbridge : MonoBehaviour
{

    [SerializeField]
    private AbstractTrigger trigger;

    private void Start()
    {
        trigger.OnTrigger += OnTrigger;
    }

    private void OnTrigger()
    {
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
