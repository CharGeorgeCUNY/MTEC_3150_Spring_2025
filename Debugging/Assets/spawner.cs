using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Meteor MeteorPrefab;
    public float distance = 14f;
    public float intervale = 0.5f;

    void Start()
    {
        InvokeRepeating("DoSpawn", 1f, intervale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoSpawn()
    {
        Vector2 RandVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        RandVector *= distance;
        Instantiate(MeteorPrefab, RandVector, Quaternion.identity);
    }
}
