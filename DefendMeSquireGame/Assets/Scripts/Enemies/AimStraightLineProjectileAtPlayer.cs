using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileFirer))]
public class AimStraightLineProjectileAtPlayer : MonoBehaviour
{

    private Vector3 _trajectoryToPlayer;

    public Vector3 Trajectory
    {
        get
        {
            AimAtPlayer();
            return _trajectoryToPlayer;
        }
    }

    private void Awake()
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
            _trajectoryToPlayer = player.transform.position - transform.position;
        }
    }
}
