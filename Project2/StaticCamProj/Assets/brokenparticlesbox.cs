using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenparticlesbox : MonoBehaviour
{
    float TimeSpawned;
    public float MyTimeAlive = 2f;
   // public float Speed = 20f;
    // Start is called before the first frame update
    void Start()



    {



        TimeSpawned = Time.timeSinceLevelLoad;
    }
    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad - TimeSpawned > MyTimeAlive)
        {
            DoDestroyTime();
        }
    }
    void DoDestroyTime()
    {
        Destroy(this.gameObject);
    }
}
