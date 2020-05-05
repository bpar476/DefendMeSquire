using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTopper : MonoBehaviour
{
    public Ladder ladder;

    private Collider2D col;

    private void Start() {
        col = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void Update()
    {
       if (ladder.isPlayerOnLadder()) {
           col.enabled = false;
       } else {
           col.enabled = true;
       }
    }
}
