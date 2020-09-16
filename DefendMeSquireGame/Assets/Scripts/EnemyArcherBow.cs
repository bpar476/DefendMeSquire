using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherBow : MonoBehaviour
{

    private static readonly float ARROW_TRAVEL_DURATION = 1.0f;

    [SerializeField]
    private Transform bow;
    private ArrowFirer firer;

    private float originalXScale;

    private void Awake()
    {
        originalXScale = transform.localScale.x;
        firer = GetComponentInChildren<ArrowFirer>();
    }

    private void FixedUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerPosition = player.transform.position;
        float direction = 1.0f;

        if (playerPosition.x > transform.position.x)
        {
            direction = -1.0f;
        }

        transform.localScale = new Vector3(direction * originalXScale, transform.localScale.y, transform.localScale.z);

        var playerRb = player.GetComponent<Rigidbody2D>();
        var aimOffset = 0f;
        // Aim a bit higher so that the arrow falls to the player
        if (playerRb.velocity.x > 1)
        {
            aimOffset = 1.0f;
        }
        else if (playerRb.velocity.x < -1)
        {
            aimOffset = -1.0f;
        }

        var playerX = playerPosition.x + aimOffset;
        var ourX = firer.transform.position.x;
        var arrowVelocityXComponent = (playerX - ourX) / ARROW_TRAVEL_DURATION;
        var playerY = playerPosition.y;
        var ourY = firer.transform.position.y;
        var yDisplacement = playerY - ourY;
        var gravity = Mathf.Abs(Physics2D.gravity.y);
        var arrowVelocityYComponent = (yDisplacement + 0.5f * gravity * ARROW_TRAVEL_DURATION * ARROW_TRAVEL_DURATION) / ARROW_TRAVEL_DURATION;

        var trajectory = new Vector3(arrowVelocityXComponent, arrowVelocityYComponent);
        firer.trajectory = trajectory;
        firer.fireVelocity = trajectory.magnitude;

        var aimAngle = Mathf.Atan(arrowVelocityYComponent / arrowVelocityXComponent);
        bow.localEulerAngles = new Vector3(0, 0, direction * aimAngle * Mathf.Rad2Deg);
    }
}
