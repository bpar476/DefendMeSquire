
using UnityEngine;

public class ArrowFirer : MonoBehaviour
{

    public int period;
    public float timerOffset;
    public Vector2 trajectory;
    public float fireVelocity;
    public GameObject projectilePrefab;
    public Color gizmoColor;
    private Vector2 normalizedTrajectory;
    private float timer;
    private bool firedFirstShot = false;
    private ArrowWarning warning;
    private bool hasWarned;

    // Start is called before the first frame update
    void Start()
    {
        warning = GetComponent<ArrowWarning>();
        timer = -timerOffset;
        normalizedTrajectory = trajectory.normalized;
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
        timer += Time.fixedDeltaTime;
        if (!firedFirstShot && timer >= 0)
        {
            return true;
        }

        if (timer > period)
        {
            timer -= period;

            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 direction = trajectory.normalized;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
