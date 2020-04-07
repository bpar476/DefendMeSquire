using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    public float grabRadiusX;
    public float grabRadiusY;

    private Vector3 left {
        get {
            return transform.position - new Vector3(grabRadiusX, 0, 0);
        }
    }

    private Vector3 right {
        get {
            return transform.position + new Vector3(grabRadiusX, 0, 0);
        }
    }

    private Vector3 bottom {
        get {
            return transform.position - new Vector3(0, grabRadiusY, 0);
        }
    }

    private Vector3 top {
        get {
            return transform.position + new Vector3(0, grabRadiusY, 0);
        }
    }

    private Vector3 topRight {
        get {
            return top + new Vector3(0, grabRadiusX, 0);
        }
    }

    private Vector3 bottomLeft {
        get {
            return bottom - new Vector3(0, grabRadiusX, 0);
        }
    }

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Bounds bounds = new Bounds(transform.position, new Vector3(grabRadiusX * 2, grabRadiusY * 2, 0));
        if (bounds.Contains(player.transform.position)) {

        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(left, right);
        Gizmos.DrawLine(bottom, top);
    }
}
