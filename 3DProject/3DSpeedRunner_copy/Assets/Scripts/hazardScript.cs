using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardScript : MonoBehaviour
{
    GameObject player;
    //public GameObject hazard;
    Camera cam;

    private void OnEnable()
    {
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("camPos");
        Debug.Log(cam.transform.position.z);
        Debug.Log("playerPos");
        Debug.Log(player.transform.position.z);
        //(gameObject.transform.position.z < playerPos.position.z)
        if ((gameObject.transform.position.z > cam.transform.position.z)
            && (gameObject.transform.position.z < player.transform.position.z))
        {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
    }
}
