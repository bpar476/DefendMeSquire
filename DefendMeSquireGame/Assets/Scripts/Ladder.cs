using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed;

    private GameObject player;
    private bool playerInLadder;

    private float actualClimbSpeed;

    private void Start() {
        actualClimbSpeed = climbSpeed / 60;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0 && playerInLadder) {
            player.transform.Translate(new Vector3(0, Mathf.Sign(verticalInput) * actualClimbSpeed), 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            Debug.Log("player enters ladder");
            playerInLadder = true;

            player = other.gameObject;

            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            Debug.Log("player exits ladder");
            playerInLadder = false;

            player = other.gameObject;

            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 1;
        }
    }
}
