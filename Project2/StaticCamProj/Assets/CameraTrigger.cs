using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    bool IseePlayer = false;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && IseePlayer == false)
        {
            
            this.GetComponentInParent<CamSwitch>().Switch(true);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponentInParent<CamSwitch>().Switch(false);
        }
    }

}
