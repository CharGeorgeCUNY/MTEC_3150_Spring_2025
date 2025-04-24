using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSFX : MonoBehaviour
{
    public AudioSource auso;
    // Start is called before the first frame update
    void Start()
    {
        auso = this.GetComponent<AudioSource>();
    }

    
}
