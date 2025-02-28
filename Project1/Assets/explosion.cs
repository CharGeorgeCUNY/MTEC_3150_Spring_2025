using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    
    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

}
