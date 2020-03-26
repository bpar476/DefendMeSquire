using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFirer : MonoBehaviour
{

    public Vector2 trajectory;
    public float fireVelocity;
    public GameObject projectilePrefab;
    private Vector2 normalizedTrajectory;

    // Start is called before the first frame update
    void Start()
    {
        normalizedTrajectory = trajectory.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            fireProjectile();
        }
    }

    private void fireProjectile() {
        GameObject projectile = Instantiate(projectilePrefab) as GameObject;

        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        Debug.Log(normalizedTrajectory);
        projectileBody.velocity = fireVelocity * normalizedTrajectory;
    }
}
