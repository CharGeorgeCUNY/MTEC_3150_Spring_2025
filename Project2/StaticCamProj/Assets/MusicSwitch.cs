using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<GameManager>().music = 1;
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<GameManager>().music = 0;
    }
}
