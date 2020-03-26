using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 cachedVelocity;
    private bool collided;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        if (velocityMeaningfullyChanged(currentVelocity)) {
            float angle = Vector2.SignedAngle(Vector2.right, currentVelocity);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private bool velocityMeaningfullyChanged(Vector2 currentVelocity) {
        bool xMeaningfullyChanged = floatsMeaningfullyDifferent(currentVelocity.x, cachedVelocity.x, 0.1f);
        bool yMeaningfullyChanged = floatsMeaningfullyDifferent(currentVelocity.y, cachedVelocity.y, 0.1f);
        return xMeaningfullyChanged || yMeaningfullyChanged;
    }

    private bool floatsMeaningfullyDifferent(float a, float b, float tolerance) {
        float difference = a - b;
        float absTolerance = Mathf.Abs(tolerance);
        return difference > absTolerance || difference < 0 - absTolerance;
    }
}
