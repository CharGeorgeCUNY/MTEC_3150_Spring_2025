using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lookat : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        this.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
    }

   
}
