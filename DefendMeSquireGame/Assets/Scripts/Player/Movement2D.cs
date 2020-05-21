using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float speed;
    public float maxMoveSpeed;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentDir = Mathf.Sign(rb2d.velocity.x);

        if (IsFalling())
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        else
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                float inputDir = Mathf.Sign(Input.GetAxis("Horizontal"));
                if (inputDir * rb2d.velocity.x < maxMoveSpeed)
                {
                    rb2d.AddForce(new Vector2(inputDir * speed, 0));
                }

                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    private bool IsFalling()
    {
        return rb2d.velocity.y < -0.01;
    }
}
