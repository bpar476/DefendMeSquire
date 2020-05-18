using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireTaskSpawnPoint : MonoBehaviour
{
    public Vector2 spawnLocation;
    public int spawnDelaySeconds;
    public SquireTask taskPrefab;

    public SquireTask Spawn()
    {
        SquireTask task = Instantiate(taskPrefab, transform.position, Quaternion.identity);

        return task;
    }
}
