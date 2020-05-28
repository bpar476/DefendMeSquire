using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var killable = other.gameObject.GetComponent<Killable>();
        if (killable != null)
        {
            killable.Kill();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
