using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PointProjectileWithVelocity : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 cachedVelocity;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = rb2d.velocity;
        if (velocityMeaningfullyChanged(currentVelocity))
        {
            updateRotationBasedOnVelocity(currentVelocity);
        }
        cachedVelocity = currentVelocity;
    }

    private bool velocityMeaningfullyChanged(Vector2 currentVelocity)
    {
        bool xMeaningfullyChanged = floatsMeaningfullyDifferent(currentVelocity.x, cachedVelocity.x, 0.1f);
        bool yMeaningfullyChanged = floatsMeaningfullyDifferent(currentVelocity.y, cachedVelocity.y, 0.1f);
        return xMeaningfullyChanged || yMeaningfullyChanged;
    }

    private void updateRotationBasedOnVelocity(Vector2 velocity)
    {
        float angle = Vector2.SignedAngle(Vector2.right, velocity);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private bool floatsMeaningfullyDifferent(float a, float b, float tolerance)
    {
        float difference = a - b;
        float absTolerance = Mathf.Abs(tolerance);
        return difference > absTolerance || difference < 0 - absTolerance;
    }
}
