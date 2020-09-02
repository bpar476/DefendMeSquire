using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip impactSound;
    public AudioClip fleshImpactSound;
    /// <summary>
    /// This sets the launch velocity which will be applied to the arrow once it has finished its summon animation via the LaunchArrow behaviour.
    /// </summary>
    public Vector2 launchVelocity
    {
        get { return _launchVelocity; }
        set
        {
            _launchVelocity = value;
            updateRotationBasedOnVelocity(value);
        }
    }
    private Vector2 _launchVelocity;
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
        GetComponent<Animator>().enabled = false;
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

    /// <summary>

    /// </summary>
    /// <param name="velocity"></param>
    public void SetLaunchVelocity(Vector2 velocity)
    {
        launchVelocity = velocity;
    }

    void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        if (velocityMeaningfullyChanged(currentVelocity) && !collided)
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
