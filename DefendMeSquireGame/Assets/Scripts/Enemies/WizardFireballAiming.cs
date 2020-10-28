using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimStraightLineProjectileAtPlayer), typeof(ProjectileFirer))]
public class WizardFireballAiming : MonoBehaviour
{

    private ProjectileFirer firer;
    private AimStraightLineProjectileAtPlayer aimAtPlayer;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        firer = GetComponent<ProjectileFirer>();
        aimAtPlayer = GetComponent<AimStraightLineProjectileAtPlayer>();
    }

    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerRb != null)
        {
            var vectorToPlayer = aimAtPlayer.Trajectory;
            var playerVelocity = playerRb.velocity;
            var aimOffset = new Vector3(signOrZero(playerVelocity.x) * 1.5f, 0, 0);

            firer.trajectory = vectorToPlayer + aimOffset;
        }
    }

    private float signOrZero(float x)
    {
        return Mathf.Abs(x) <= 0.1f ? 0 : Mathf.Sign(x);
    }
}
