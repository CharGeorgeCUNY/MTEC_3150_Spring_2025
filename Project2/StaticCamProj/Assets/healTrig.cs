using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healTrig : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().Health = 100;
        }
    }
}
