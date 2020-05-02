using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFirer : MonoBehaviour
{

    public int shotsPerMinute;
    public Vector2 trajectory;
    public float fireVelocity;
    public GameObject projectilePrefab;
    private Vector2 normalizedTrajectory;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        normalizedTrajectory = trajectory.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasTimerTicked()) {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand <= shotsPerMinute / 60.0f) {
                fireProjectile();
            }
        }
    }

    private void fireProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;

        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        projectileBody.velocity = fireVelocity * normalizedTrajectory;
    }

    bool HasTimerTicked() {
        timer += Time.fixedDeltaTime;
        if (timer > 1.0f) {
            timer -= 1.0f;

            return true;
        }
        return false;
    }
}
