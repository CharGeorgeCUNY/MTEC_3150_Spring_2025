using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [Header("Map Settings")]
    public GameObject mapCube;

    [Header("Gravestones (array of variants)")]
    public GameObject[] gravestonePrefabs;
    public int gravestoneCount = 5;

    [Header("Cacti (array of variants)")]
    public GameObject[] cactusPrefabs;
    public int cactusCount = 10;

    [Header("Rocks (array of variants)")]
    public GameObject[] rockPrefabs;
    public int rockCount = 10;

    [Header("Small Structures (array of variants)")]
    public GameObject[] structurePrefabs;
    public int structureCount = 3;

    [Header("NavMesh Settings")]
    public NavMeshSurface navMeshSurface; // Reference to your NavMeshSurface

    private Vector3 cubeSize;
    private Vector3 cubeCenter;

    // Optional: If you want to force the raycast to only hit the map:
    // public LayerMask mapLayerMask;

    void Start()
    {
        if (mapCube == null)
        {
            Debug.LogError("Map Cube is not assigned in SpawnManager.");
            return;
        }

        // Read the cube’s size and center so we know where to spawn
        Renderer rend = mapCube.GetComponent<Renderer>();
        cubeSize = rend.bounds.size;
        cubeCenter = rend.bounds.center;

        // Spawn each category of object
        SpawnObjects(gravestonePrefabs, gravestoneCount);
        SpawnObjects(cactusPrefabs, cactusCount);
        SpawnObjects(rockPrefabs, rockCount);
        SpawnObjects(structurePrefabs, structureCount);

        // --- DYNAMIC NAVMESH BAKE ---
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogWarning("NavMeshSurface reference not set on SpawnManager.");
        }
    }

    private void SpawnObjects(GameObject[] prefabs, int count)
    {
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogWarning("No prefabs assigned for this category.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            // Pick random X/Z within the horizontal bounds of the cube
            float randomX = Random.Range(-cubeSize.x / 2f, cubeSize.x / 2f);
            float randomZ = Random.Range(-cubeSize.z / 2f, cubeSize.z / 2f);

            // The top surface is at (cubeCenter.y + cubeSize.y/2)
            float ySurface = cubeCenter.y + cubeSize.y / 2f;

            // Start spawnPos at the top center + offset in X/Z
            Vector3 spawnPos = new Vector3(
                cubeCenter.x + randomX,
                ySurface,
                cubeCenter.z + randomZ
            );

            // Raycast from FAR above to ensure we always go top-down
            Vector3 rayOrigin = new Vector3(spawnPos.x, ySurface + 100f, spawnPos.z);

            RaycastHit hit;
            // If you want to restrict the ray to only hit the map, pass a layer mask:
            // if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 200f, mapLayerMask))
            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 200f))
            {
                // Optional: Check if we really hit the mapCube (or the top of it).
                // if (hit.collider.gameObject == mapCube)
                // {
                spawnPos = hit.point;
                // }
                // else
                // {
                //     // If we didn't hit the mapCube, fallback to ySurface
                //     spawnPos.y = ySurface;
                // }
            }
            else
            {
                // Fallback: If raycast fails, default to the top surface
                spawnPos.y = ySurface;
            }

            // Randomly pick one of the prefabs
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

            // Instantiate
            Instantiate(randomPrefab, spawnPos, Quaternion.identity);
        }
    }
}
