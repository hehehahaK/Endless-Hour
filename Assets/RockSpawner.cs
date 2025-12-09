using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnRock", 1f, spawnInterval);
    }

    void SpawnRock()
    {
        Instantiate(rockPrefab, transform.position, Quaternion.identity);
    }
}

