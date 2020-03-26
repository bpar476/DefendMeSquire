using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float angle = Vector2.SignedAngle(Vector2.right, rb.velocity);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
