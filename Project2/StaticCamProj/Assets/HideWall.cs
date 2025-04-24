using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWall : MonoBehaviour
{
    public void Hide(bool on)
    {
        if (on == true)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (on == false)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
}
