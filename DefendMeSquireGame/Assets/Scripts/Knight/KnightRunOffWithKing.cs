using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRunOffWithKing : MonoBehaviour
{

    [SerializeField]
    public Transform king;

    public void FleeToVictory()
    {
        king.transform.SetParent(transform);
        king.transform.localPosition = new Vector3(0.6f, -0.4f, 0);
        StartCoroutine(JumpToTheLeft());
    }

    private IEnumerator JumpToTheLeft()
    {
        for (var i = 0; i < (5 * 100); i++)
        {
            transform.position += new Vector3(-0.15f, 0.1f, 0);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
