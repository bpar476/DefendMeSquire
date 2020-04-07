using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();

        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0) {

                rb2d.velocity = new Vector2(0, Mathf.Sign(verticalInput) * climbSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            Debug.Log("player enters ladder");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            Debug.Log("player exits ladder");
        }
    }
}
