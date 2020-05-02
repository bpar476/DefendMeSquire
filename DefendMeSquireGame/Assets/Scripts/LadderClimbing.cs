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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, reach, ladderMask);
        if (hit.collider != null && climbUp) {
            // Can climb the ladder
            ClimbUp();
        }
    }

    private void ClimbUp() {
        body.MovePosition(new Vector2(transform.position.x, transform.position.y) + (new Vector2(0, 1) * climbSpeed));
    }
}
