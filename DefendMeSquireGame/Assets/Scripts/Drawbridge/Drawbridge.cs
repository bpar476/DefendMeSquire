using System.Collections;
using UnityEngine;

/// <summary>
/// Lowers the drawbridge. Invokes its trigger after drawbridge has finished lowering.
/// </summary>
public class Drawbridge : AbstractTrigger
{

    [SerializeField]
    private AbstractTrigger trigger;

    private void Start()
    {
        trigger.OnTrigger += StartLowerDrawbridge;
    }

    private void StartLowerDrawbridge()
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

        OnTrigger?.Invoke();

        yield return null;
    }
}
