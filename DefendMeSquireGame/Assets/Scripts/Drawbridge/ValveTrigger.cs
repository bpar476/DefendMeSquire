using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveTrigger : MonoBehaviour
{

    [SerializeField]
    private Transform valve;
    [SerializeField]
    private float crankTime;
    [SerializeField]
    private AbstractTriggeredEvent onFinishCranking;
    private bool isPlayerOnValve = false;
    private bool cranking = false;
    private float crankProgress = 0;
    private bool rollingBack = false;
    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerOnValve = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerOnValve = false;
    }

    void Update()
    {
        if (!completed && isPlayerOnValve && !rollingBack && Input.GetKey(KeyCode.E))
        {
            Crank();
        }
        else if (cranking)
        {
            ResetCrank();
        }
    }

    private void Crank()
    {
        cranking = true;


        IncrementCrankProgress();

        RotateValve();

        if (crankProgress >= crankTime)
        {
            CompleteCrank();
        }
    }

    private void IncrementCrankProgress()
    {
        crankProgress += Time.deltaTime;
    }

    private void RotateValve()
    {
        var rotation = valve.localRotation.eulerAngles;
        valve.localRotation = Quaternion.Euler(rotation.x, rotation.y, 360 * crankProgress / crankTime);
    }

    private void ResetCrank()
    {
        cranking = false;
        crankProgress = 0;
        rollingBack = true;
        StartCoroutine(RollBackValve());
    }

    private void CompleteCrank()
    {
        onFinishCranking.OnTrigger();
        completed = true;
    }

    private IEnumerator RollBackValve()
    {
        for (float i = 0; i < 0.5; i += Time.deltaTime)
        {
            var rotation = valve.localRotation.eulerAngles;
            valve.localRotation = Quaternion.Euler(rotation.x, rotation.y, Mathf.Lerp(rotation.z, 0, 2 * i));
            yield return null;
        }

        rollingBack = false;
        yield return null;
    }
}
