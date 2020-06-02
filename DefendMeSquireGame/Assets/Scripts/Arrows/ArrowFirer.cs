
using UnityEngine;

public class ArrowFirer : MonoBehaviour
{
    public Timer timer;
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

    // Start is called before the first frame update
    void Start()
    {
        warning = GetComponent<ArrowWarning>();
        normalizedTrajectory = trajectory.normalized;
    }

    private void OnEnable()
    {
        timer.StartTimer();
    }

    private void OnDisable()
    {
        timer.ResetAndStopTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!(firedFirstShot || hasWarned))
        {
            warning.ActivateWarning();
            hasWarned = true;
        }

        if (HasTimerTicked())
        {
            fireProjectile();
        }
    }

    private void fireProjectile()
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

    bool HasTimerTicked()
    {
        bool tick = timer.Ticked();
        return timer.Ticked();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 direction = trajectory.normalized;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
