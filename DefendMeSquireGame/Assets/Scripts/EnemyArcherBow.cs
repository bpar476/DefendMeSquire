using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherBow : MonoBehaviour
{
    [SerializeField]
    private Transform bow;

    private float originalXScale;

    private void Awake()
    {
        originalXScale = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float direction = 1.0f;

        if (playerPosition.x > transform.position.x)
        {
            direction = -1.0f;
        }

        transform.localScale = new Vector3(direction * originalXScale, transform.localScale.y, transform.localScale.z);

        var lineFromBowToPlayer = playerPosition - bow.transform.position;
        var angleBetweenBowAndPlayer = Vector2.Angle(direction * Vector2.left, lineFromBowToPlayer);
        bow.localEulerAngles = new Vector3(0, 0, angleBetweenBowAndPlayer);
    }
}
