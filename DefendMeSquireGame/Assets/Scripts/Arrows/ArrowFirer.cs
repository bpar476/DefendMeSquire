
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
        warning.ActivateWarning();
        if (HasTimerTicked())
        {
            fireProjectile();
            warning.DeactivateWarning();
        }
    }

    private void fireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;

        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        projectileBody.velocity = fireVelocity * normalizedTrajectory;
    }

    bool HasTimerTicked()
    {
        timer += Time.fixedDeltaTime;
        if (!firedFirstShot && timer >= 0)
        {
            firedFirstShot = true;
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
