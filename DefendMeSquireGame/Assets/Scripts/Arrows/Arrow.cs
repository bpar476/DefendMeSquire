using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip impactSound;
    public AudioClip fleshImpactSound;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Vector2 cachedVelocity;
    private bool collided;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            audioSource.clip = fleshImpactSound;
        }
        else
        {
            audioSource.clip = impactSound;
        }
        audioSource.Play();
        ContactPoint2D contact = other.GetContact(0);
        Vector2 contactWorldPosition = contact.point;
        Vector2 contactLocalPosition = other.transform.worldToLocalMatrix.MultiplyPoint(contactWorldPosition);
        transform.SetParent(other.transform);
        transform.localPosition = contactLocalPosition;

        rb.isKinematic = false;
        rb.simulated = false;
        collided = true;
        GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        if (velocityMeaningfullyChanged(currentVelocity) && !collided)
        {
            float angle = Vector2.SignedAngle(Vector2.right, currentVelocity);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        cachedVelocity = currentVelocity;
    }

    private bool velocityMeaningfullyChanged(Vector2 currentVelocity)
    {
        bool xMeaningfullyChanged = floatsMeaningfullyDifferent(currentVelocity.x, cachedVelocity.x, 0.1f);
        bool yMeaningfullyChanged = floatsMeaningfullyDifferent(currentVelocity.y, cachedVelocity.y, 0.1f);
        return xMeaningfullyChanged || yMeaningfullyChanged;
    }

    private bool floatsMeaningfullyDifferent(float a, float b, float tolerance)
    {
        float difference = a - b;
        float absTolerance = Mathf.Abs(tolerance);
        return difference > absTolerance || difference < 0 - absTolerance;
    }
}
