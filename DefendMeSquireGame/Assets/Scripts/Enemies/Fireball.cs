using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer), typeof(Collider2D))]
public class Fireball : MonoBehaviour
{

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
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
        audioSource.Play();
        spriteRenderer.enabled = false;
        collider.enabled = false;
        StartCoroutine(DestroyFireBallAfterClipFinished());
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
