
using UnityEngine;

public class ArrowFirer : MonoBehaviour, GlobalTimerStopwatch
{
    public int period;
    public float timerOffset;
    public Vector2 trajectory;
    public float fireVelocity;
    public GameObject projectilePrefab;
    public Color gizmoColor;
    private Vector2 normalizedTrajectory;
    private bool firedFirstShot = false;
    private ArrowWarning warning;
    private bool hasWarned;
    private int stopwatchId;
    private GlobalTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        normalizedTrajectory = trajectory.normalized;
    }

    private void OnEnable()
    {
        GetWarning().ActivateWarning();
        stopwatchId = GetTimer().AddStopwatch(this);
    }

    private void OnDisable()
    {
        var warning = GetWarning();
        var timer = GetTimer();
        if (warning != null)
        {
            warning.DeactivateWarning();
        }
        if (timer != null)
        {
            timer.RemoveStopwatch(stopwatchId);
        }
    }

    public void OnTick()
    {
        GetWarning().DeactivateWarning();
        FireProjectile();
    }

    public float Period()
    {
        return period;
    }

    public float Offset()
    {
        return timerOffset;
    }
    private void FireProjectile()
    {
        if (!firedFirstShot)
        {
            firedFirstShot = true;
            warning.DeactivateWarning();
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;

        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        projectileBody.velocity = fireVelocity * normalizedTrajectory;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 direction = trajectory.normalized;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }

    private GlobalTimer GetTimer()
    {
        // if (timer == null)
        {
            timer = FindObjectOfType<GlobalTimer>();
        }
        return timer;
    }

    private ArrowWarning GetWarning()
    {
        if (warning == null)
        {
            warning = GetComponent<ArrowWarning>();
        }
        return warning;
    }
}
