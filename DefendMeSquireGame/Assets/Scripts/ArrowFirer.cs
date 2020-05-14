using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFirer : MonoBehaviour
{

    public int period;
    public float timerOffset;
    public Vector2 trajectory;
    public float fireVelocity;
    public GameObject projectilePrefab;
    private Vector2 normalizedTrajectory;
    private float timer;
    private bool firedFirstShot = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = -timerOffset;
        normalizedTrajectory = trajectory.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasTimerTicked())
        {
            // float rand = Random.Range(0.0f, 1.0f);
            // if (rand <= shotsPerMinute / 60.0f) {
            fireProjectile();
            // }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 direction = trajectory.normalized;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
