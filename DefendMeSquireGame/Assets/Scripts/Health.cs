using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHitPoints;
    public Text hitpointsUI;

    private int currentHitPoints;

    private void Start() {
        currentHitPoints = maxHitPoints;
        UpdateHitpointsUI();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Damage damage = other.gameObject.GetComponent<Damage>();
        if (damage != null) {
            currentHitPoints -= damage.hits;

            UpdateHitpointsUI();

            if (currentHitPoints <= 0) {
                Die();
            }
        }
    }

    private void UpdateHitpointsUI() {
        hitpointsUI.text = currentHitPoints.ToString();

        if (currentHitPoints == maxHitPoints) {
            hitpointsUI.color = Color.green;
        } else if (currentHitPoints == 0) {
            hitpointsUI.color = Color.red;
        } else {
            hitpointsUI.color = Color.yellow;
        }
    }

    private void Die() {
        Debug.Log("you ded");
    }
}
