using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTopper : MonoBehaviour
{
    public Ladder ladder;

    private PlatformEffector2D effector2D;

    private void Start() {
        effector2D = GetComponent<PlatformEffector2D>();
    }


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        if (playerPosition.y > transform.position.y + 0.5f && Input.GetAxis("Vertical") < 0) {
            // Player is above tip of ladder
            effector2D.rotationalOffset = 180;
        } else {
            effector2D.rotationalOffset = 0;
        }
    }
}
