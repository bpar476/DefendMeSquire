using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : Projectile
{
    [SerializeField]
    private AudioClip castClip;
    [SerializeField]
    private AudioClip impactClip;

    [SerializeField]
    private float fireballDuration;
    private float fireTime;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;
    private Rigidbody2D rb2d;
    private bool hasDetonated;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.velocity = launchVelocity;
        fireTime = Time.time;
        audioSource.clip = castClip;
        audioSource.Play();
    }

    private void Update()
    {
        if (!hasDetonated)
        {
            if (Time.time - fireTime > fireballDuration)
            {
                DetonateFireball();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DetonateFireball();
        }
    }

    private void DetonateFireball()
    {
        if (!hasDetonated)
        {
            hasDetonated = true;
            audioSource.clip = impactClip;
            audioSource.Play();
            spriteRenderer.enabled = false;
            collider.enabled = false;
            StartCoroutine(DestroyFireBallAfterClipFinished());
        }
    }

    private IEnumerator DestroyFireBallAfterClipFinished()
    {

        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }
}
