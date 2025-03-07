using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    public GameObject fallingObjectPrefab;
    public float spawnRate = 10f;
    public float spawnRangeX = 5f;
    

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 1f, spawnRate);
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 3f, 0);
        Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);
    }
}