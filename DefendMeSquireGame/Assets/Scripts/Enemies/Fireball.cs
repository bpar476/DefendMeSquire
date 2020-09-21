using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Fireball : Projectile
{
    private static readonly string ANIM_TRIGGER_EXPLODE = "Impact";

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
    private float rotation = 0;
    private Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        rotation += 2f;
        transform.localEulerAngles = new Vector3(0, 0, -(rotation % 360));
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
            rb2d.velocity = Vector2.zero;
            hasDetonated = true;
            animator.SetTrigger(ANIM_TRIGGER_EXPLODE);
            audioSource.clip = impactClip;
            audioSource.Play();
            collider.enabled = false;
        }
    }

    // Called by fireball StateMachineBehaviour
    public void OnExplosionFinished()
    {
        spriteRenderer.enabled = false;
        StartCoroutine(DestroyFireBallAfterClipFinished());
    }

    private IEnumerator DestroyFireBallAfterClipFinished()
    {

        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GameObject.Destroy(this.gameObject);
    }
}
