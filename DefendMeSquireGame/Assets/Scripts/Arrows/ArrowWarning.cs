using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWarning : MonoBehaviour, GlobalTimerStopwatch
{
    private static int numFlickers = 3;
    public GameObject warningTemplate;
    private ArrowFirer firer;
    private Camera cam;
    private GameObject warning;
    private bool warningActive = false;

    private bool warningVisible = false;
    private int flickerCount = 0;

    private int timerStopwatchId;
    private GlobalTimer timer;

    private void Start()
    {
        Initialise();
    }

    private void Initialise()
    {
        firer = GetComponent<ArrowFirer>();
        cam = Camera.main;
    }

    public void ActivateWarning()
    {
        // TODO: I should be initialised by a lifecycle method before I'm called
        if (firer == null || cam == null)
        {
            Initialise();
        }
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

        warningVisible = true;

        timer = GameObject.FindObjectOfType<GlobalTimer>();
        timerStopwatchId = timer.AddStopwatch(this);
    }

    public void DeactivateWarning()
    {
        if (warningActive)
        {
            Object.Destroy(warning.gameObject);
            // FIXME: I should go in initialisation code but it causes issues
            {
                warningActive = false;
                warningVisible = false;
                flickerCount = 0;
            }
            timer.RemoveStopwatch(timerStopwatchId);
        }
    }

    public void OnTick()
    {
        if (warningActive)
        {
            ToggleWarningRendered();

            UpdateFlicker();
        }
    }

    private void ToggleWarningRendered()
    {
        warningVisible = !warningVisible;
        warning.GetComponent<SpriteRenderer>().enabled = warningVisible;
    }

    private void UpdateFlicker()
    {
        if (warningVisible)
        {
            flickerCount++;
            if (flickerCount == numFlickers)
            {
                DeactivateWarning();
            }
        }
    }

    public float Period()
    {
        return 0.5f;
    }

    public float Offset()
    {
        return 0;
    }

    private (Vector2 intersection, bool found) GetWarningPosition()
    {
        var cam = Camera.main;
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


    private void OnDrawGizmosSelected()
    {
        if (firer == null)
        {
            Initialise();
        }

        var (origin, found) = GetWarningPosition();

        if (found)
        {
            Gizmos.DrawSphere(origin, 0.05f);
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
}
