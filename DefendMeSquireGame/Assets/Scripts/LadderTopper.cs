using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTopper : MonoBehaviour
{
    public Ladder ladder;

    private bool effectorModified = false;
    private PlatformEffector2D effector2D;

    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            // Player is dead
            gameObject.SetActive(false);
            return;
        }

        Vector3 playerPosition = player.transform.position;
        if (playerPosition.y > transform.position.y + 0.5f && Input.GetAxis("Vertical") < 0)
        {

            // Setting the rotation of the effector repeatedly messes with it's collision properties
            if (!effectorModified)
            {
                effector2D.rotationalOffset = 180;
                effectorModified = true;
            }
        }
        else
        {
            if (effectorModified)
            {
                effector2D.rotationalOffset = 0;
                effectorModified = false;
            }
        }
    }
}
