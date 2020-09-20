using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileFirer))]
public class AimStraightLineProjectileAtPlayer : MonoBehaviour
{

    private ProjectileFirer firer;

    private void Awake()
    {
        firer = GetComponent<ProjectileFirer>();
        AimAtPlayer();
    }

    private void Update()
    {
        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("player is null. Disabling enemy aiming");
            this.enabled = false;
        }
        else
        {
            firer.trajectory = player.transform.position - transform.position;
        }
    }
}
