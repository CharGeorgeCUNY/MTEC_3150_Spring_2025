
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class eyeofRandom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            string[] first = { "Puzzle", "Action", "Platformer" };
            Debug.Log(first[UnityEngine.Random.Range(0, first.Length)]);
            string[] second = { "Fixed", "First", "Third" };
            Debug.Log(second[UnityEngine.Random.Range(0, second.Length)]);

        }
    }
}
