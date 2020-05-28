using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWarning : MonoBehaviour
{
    public GameObject warningTemplate;
    private ArrowFirer firer;
    private Camera cam;
    private GameObject warning;
    private bool warningActive = false;

    // Start is called before the first frame update
    void Start()
    {
        firer = GetComponent<ArrowFirer>();
        cam = Camera.main;
    }

    public void ActivateWarning()
    {
        warningActive = true;

        var (intersection, found) = GetWarningPosition();
        if (found)
        {
            warning = Instantiate(warningTemplate, intersection, Quaternion.identity);
        }
        else
        {
            warning = Instantiate(warningTemplate, transform.position, Quaternion.identity);
        }
    }

    public void DeactivateWarning()
    {
        if (warningActive)
        {
            Object.Destroy(warning.gameObject);
            warningActive = false;
        }
    }

    private (Vector2 intersection, bool found) GetWarningPosition()
    {
        float width = cam.orthographicSize * cam.aspect;
        // Decrease width by a bit so sign is in view
        width -= 0.1f;
        float height = cam.orthographicSize / cam.aspect;

        int side = transform.position.x < -width ? -1 : 1;

        Vector2 direction = firer.trajectory.normalized * 10;
        Vector2 projection = new Vector2(transform.position.x, transform.position.y) + direction;
        Vector2 cameraEdgeBottom = new Vector2(side * width, -height);
        Vector2 cameraEdgeHeight = new Vector2(side * width, height);

        bool found;
        Vector2 intersection = GetIntersectionPointCoordinates(transform.position, projection, cameraEdgeBottom, cameraEdgeHeight, out found);

        return (intersection, found);
    }

    private void OnDrawGizmos()
    {
        if (firer == null)
        {
            Start();
        }

        var (origin, found) = GetWarningPosition();

        if (found)
        {
            Gizmos.DrawSphere(origin, 0.05f);
        }
        else
        {
            Debug.Log("no intersection with camera edge");
        }
    }

    public Vector2 GetIntersectionPointCoordinates(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2, out bool found)
    {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

        if (tmp == 0)
        {
            // No solution!
            found = false;
            return Vector2.zero;
        }

        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;

        found = true;

        return new Vector2(
            B1.x + (B2.x - B1.x) * mu,
            B1.y + (B2.y - B1.y) * mu
        );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
