using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, Killable
{
    public int maxHitPoints;
    public Text hitpointsUI;
    public RectTransform deathMenu;

    private int currentHitPoints;

    private void Start()
    {
        currentHitPoints = maxHitPoints;
        UpdateHitpointsUI();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Damage damage = other.gameObject.GetComponent<Damage>();
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
        hitpointsUI.text = currentHitPoints.ToString();

        if (currentHitPoints == maxHitPoints)
        {
            hitpointsUI.color = Color.green;
        }
        else if (currentHitPoints == 0)
        {
            hitpointsUI.color = Color.red;
        }
        else
        {
            hitpointsUI.color = Color.yellow;
        }
    }
    private void Die()
    {
        Kill();
    }

    public void Kill()
    {
        deathMenu.gameObject.SetActive(true);
        StartCoroutine(KillPlayerAfterAllArrowSoundsStopped());
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
