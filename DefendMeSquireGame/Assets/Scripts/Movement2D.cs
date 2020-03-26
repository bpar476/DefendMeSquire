using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentVertVelocity = rb2d.velocity.y;
        if (currentVertVelocity == 0) {
            float horizontalVelocity = Input.GetAxis("Horizontal") * speed;
            rb2d.velocity = new Vector2(horizontalVelocity, currentVertVelocity);
        } else {
            rb2d.velocity = new Vector2(0, currentVertVelocity);
        }

    }
}
