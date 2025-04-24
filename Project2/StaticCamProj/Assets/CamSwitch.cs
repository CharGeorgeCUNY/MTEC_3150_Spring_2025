using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
      //  this.GetComponentInChildren<AudioListener>().enabled = false;
        this.GetComponentInChildren<Camera>().enabled = false;
    }
    public void Switch(bool on)
    {
        if (on)
        {
            this.GetComponentInChildren<Camera>().enabled = true;
            this.GetComponentInChildren<HideWall>().Hide(true);
           // this.GetComponentInChildren<AudioListener>().enabled = true;

        }
        else
        {
            this.GetComponentInChildren<Camera>().enabled = false;
            this.GetComponentInChildren<HideWall>().Hide(false);
          //  this.GetComponentInChildren<AudioListener>().enabled = false;
        }
        
    }

    
}
