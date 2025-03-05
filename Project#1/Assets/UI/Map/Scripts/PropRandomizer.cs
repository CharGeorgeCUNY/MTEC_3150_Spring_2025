using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (var sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;

            Collider2D col = prop.GetComponent<Collider2D>();
            AstarPath.active.UpdateGraphs(col.bounds);
        }
    }
}
