using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravestoneSpawner : MonoBehaviour
{
    [Header("Zombies")]
    // Array of zombie prefabs (different models)
    public GameObject[] zombiePrefabs;

    [Header("Spawn Settings")]
    public float spawnInterval = 5f;    // Seconds between zombie spawns
    public int maxZombies = 5;         // Maximum zombies active at once

    private int currentZombies;

    void Start()
    {
        // Start repeatedly calling SpawnZombie
        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
    }

    private void SpawnZombie()
    {
        // Only spawn if we haven't reached our max
        if (currentZombies >= maxZombies)
            return;

        // Ensure we have at least one zombie prefab
        if (zombiePrefabs == null || zombiePrefabs.Length == 0)
        {
            Debug.LogWarning("No zombie prefabs assigned to GravestoneSpawner!");
            return;
        }

        // Pick a random zombie variant from the array
        GameObject randomZombie = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

        // Instantiate a zombie at the gravestone's position
        Instantiate(randomZombie, transform.position, Quaternion.identity);
        currentZombies++;
    }

    // If a zombie is destroyed or self-destructs, call this
    public void OnZombieDestroyed()
    {
        currentZombies--;
        if (currentZombies < 0) currentZombies = 0;
    }
}
