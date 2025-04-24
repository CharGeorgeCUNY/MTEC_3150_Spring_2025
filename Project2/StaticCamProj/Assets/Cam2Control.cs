using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Cam2Control : MonoBehaviour
{
    CinemachineVirtualCamera VC1;
    CinemachineVirtualCamera VC2;
    CinemachineVirtualCamera VC3;
   
    private void OnTriggerEnter(Collider other)
    {
        VC1 = GameObject.Find("VC1").GetComponent<CinemachineVirtualCamera>();
        VC2 = GameObject.Find("VC2").GetComponent<CinemachineVirtualCamera>();
        VC3 = GameObject.Find("VC3").GetComponent<CinemachineVirtualCamera>();

        if (other.tag == "Player")
        {
            
            VC1.enabled = false;
            VC2.enabled = true;
            VC3.enabled = false;
        }

    }

}
