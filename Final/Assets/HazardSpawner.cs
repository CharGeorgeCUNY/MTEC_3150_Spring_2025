using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardSpawner2D : MonoBehaviour
{
    [Header("Prefab & Pool")]
    public GameObject hazardPrefab;
    public int poolSize = 10;
    private List<GameObject> hazardPool;

    [Header("Spawn Area")]
    public float spawnDistance = 10f;     // how far in front of the camera
    public float minY = -4f, maxY = 4f;
    public float spawnZ = 3f;

    [Header("Timing")]
    public float minInterval = 1f, maxInterval = 2f;

    void Awake()
    {
        // build a simple pool
        hazardPool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(hazardPrefab);
            go.SetActive(false);
            hazardPool.Add(go);
        }
    }

    void Start()
    {
        // immediate first spawn
        SpawnOne();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float wait = Random.Range(minInterval, maxInterval);
            Debug.Log($"[Spawner] waiting {wait:F2}s");
            yield return new WaitForSeconds(wait);
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        var cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No Main Camera!");
            return;
        }

        // pick a random vertical spot in viewport (0–1)
        float yV = Random.Range(0f, 1f);

        Vector3 vp = new Vector3(1.20f, yV, cam.nearClipPlane + 0.1f);

        Vector3 spawnPos = cam.ViewportToWorldPoint(vp);
        spawnPos.z = 3f;

        Debug.Log($"[Spawner] Spawning hazard at {spawnPos}");
        var h = GetPooledHazard();
        h.transform.position = spawnPos;
        h.SetActive(true);
    }


    GameObject GetPooledHazard()
    {
        // grab the first inactive one
        foreach (var h in hazardPool)
            if (!h.activeInHierarchy)
                return h;

        // if we run out, expand
        var extra = Instantiate(hazardPrefab);
        extra.SetActive(false);
        hazardPool.Add(extra);
        return extra;
    }
}
