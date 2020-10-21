using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(Animator))]
[RequireComponent(typeof(PointProjectileWithVelocity))]
public class Arrow : Projectile
{
    public AudioClip impactSound;
    public AudioClip fleshImpactSound;
    public bool isSummoned;

    private Animator animator;
    private static readonly string BOOL_SUMMONED = "Summon";

    private AudioSource audioSource;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.SetBool(BOOL_SUMMONED, isSummoned);
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
        GetComponent<PointProjectileWithVelocity>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
