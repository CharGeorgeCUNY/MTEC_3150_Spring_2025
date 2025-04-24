using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cam3follow : MonoBehaviour
{
    GameObject player;
    Quaternion myrot;
    // Start is called before the first frame update
    void Start()
    {
        myrot = this.transform.rotation;
        player = GameObject.Find("Player");
        this.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        this.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
    }
    


}
