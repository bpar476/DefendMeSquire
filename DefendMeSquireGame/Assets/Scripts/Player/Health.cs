using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Movement2D))]
public class Health : MonoBehaviour, Killable
{
    public int maxHitPoints;
    public HitPointsUI hitpointsUI;
    public RectTransform deathMenu;
    public List<GameObject> deathListeners;

    private int currentHitPoints;
    private bool hasInit = false;

    private void Start()
    {
        currentHitPoints = maxHitPoints;
        if (CheckpointManager.Instance.HasCheckpointed())
            transform.position = new Vector3(-5.6f, 14.5f);
    }

    private void Update()
    {
        if (!hasInit)
        {
            hitpointsUI.MaxHitPoints = maxHitPoints;
            UpdateHitpointsUI();
            hasInit = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TakeDamageFromCollision(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TakeDamageFromCollision(other.gameObject);
    }

    private void TakeDamageFromCollision(GameObject other)
    {
        Damage damage = other.GetComponent<Damage>();
        if (damage != null)
        {
            currentHitPoints -= damage.hits;

            UpdateHitpointsUI();

            if (currentHitPoints <= 0)
            {
                Die();
            }
        }
    }

    private void UpdateHitpointsUI()
    {
        hitpointsUI.HitPoints = currentHitPoints;
    }
    private void Die()
    {
        Kill();
    }

    public void Kill()
    {
        deathMenu.gameObject.SetActive(true);
        FreezeMovement();
        deathListeners.ForEach(listener =>
        {
            var deathListener = listener.GetComponent<DeathListener>();
            if (deathListener != null)
            {
                deathListener.OnDeath();
            }
            else
            {
                Debug.Log("Death listener assigned to health component does not have DeathListener component");
            }
        });
        StartCoroutine(KillPlayerAfterAllArrowSoundsStopped());
    }

    private void FreezeMovement()
    {
        GetComponent<Movement2D>().enabled = false;
        foreach (var ladder in GameObject.FindObjectsOfType<Ladder>())
        {
            ladder.enabled = false;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private IEnumerator KillPlayerAfterAllArrowSoundsStopped()
    {
        var arrows = GetComponentsInChildren<Arrow>();
        foreach (var arrow in arrows)
        {
            while (arrow.GetComponent<AudioSource>().isPlaying)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        GameObject.Destroy(this.gameObject);
    }
}
