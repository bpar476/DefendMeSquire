using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : MonoBehaviour
{

    public LayerMask ladderMask;
    /// Distance from the player that they can reach a ladder
    public float reach;
    public float climbSpeed;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        float vertInput = Input.GetAxis("Vertical");
        bool climbUp = vertInput > 0;
        bool climbDown = vertInput < 0;

        bool canClimb = false;
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, Vector2.up, reach, ladderMask);
        if (hitAbove.collider != null) {
            // Can climb the ladder
            canClimb = true;
        } else {
            RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, Vector2.down, reach, ladderMask);
            Debug.Log(hitBelow.collider?.gameObject.name);
            if (hitBelow.collider != null) {
                canClimb = true;
            }
        }

        if (canClimb && climbUp) {
            ClimbUp();
        }

        if (canClimb && climbDown) {
            ClimbDown();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - reach, transform.position.z));
    }

    private void ClimbUp() {
        body.MovePosition(new Vector2(transform.position.x, transform.position.y) + (new Vector2(0, 1) * climbSpeed));
    }

    private void ClimbDown() {
        body.MovePosition(new Vector2(transform.position.x, transform.position.y) + (Vector2.down * climbSpeed));
    }
}
