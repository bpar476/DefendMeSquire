
using UnityEngine;

public class ProjectileFirer : MonoBehaviour, GlobalTimerStopwatch
{
    public int period;
    public float timerOffset;
    public Vector2 trajectory;
    public float fireVelocity;
    public GameObject projectilePrefab;
    public Color gizmoColor;
    private Vector2 normalizedTrajectory { get { return trajectory.normalized; } }
    private bool firedFirstShot = false;
    private bool hasWarned;
    private int stopwatchId;
    private GlobalTimer timer;

    private void OnEnable()
    {
        stopwatchId = GetTimer().AddStopwatch(this);
    }

    private void OnDisable()
    {
        var timer = GetTimer();
        if (timer != null)
        {
            timer.RemoveStopwatch(stopwatchId);
        }
    }

    public void OnTick()
    {
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
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Projectile>().launchVelocity = fireVelocity * normalizedTrajectory;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 direction = trajectory.normalized;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }

    private GlobalTimer GetTimer()
    {
        timer = FindObjectOfType<GlobalTimer>();
        return timer;
    }
}
