using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHitPoints;

    private int currentHitPoints;

    private void Start() {
        currentHitPoints = maxHitPoints;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Damage damage = other.gameObject.GetComponent<Damage>();
        if (damage != null) {
            currentHitPoints -= damage.hits;

            if (currentHitPoints <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        Debug.Log("you ded");
    }
}
