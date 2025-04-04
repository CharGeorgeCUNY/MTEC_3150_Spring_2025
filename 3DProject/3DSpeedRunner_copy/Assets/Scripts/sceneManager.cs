using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sceneManager : MonoBehaviour
{

    public GameObject[] hazards;
    int hazardChoice;
    int laneChoice1;
    int laneChoice2;

    //spawning
    Vector3 spawnPos;
    Vector3 spawnPos1;
    public float spawnOffset = 5.0f;

    float currentTime = 0f;
    float lastTime = 0f;
    float timeInterval = 1.5f;

    //playerMovement script referencing
    public playerMovement player;
    GameObject hazard;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2.0f, timeInterval);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        hazardChoice = Random.Range(0, 2); //index: [0,1]

        currentTime += 1;
        currentTime = Mathf.Clamp(currentTime, 0, 4);
        laneChoice1 = Random.Range(0, 3); //index: [0,1,2]
        laneChoice2 = Random.Range(0, 3);

    }

    void Spawn()
    {

        spawnPos = new Vector3(player.changeLane[laneChoice1].position.x,player.transform.position.y, player.transform.position.z + spawnOffset);
        Instantiate(hazards[hazardChoice], spawnPos, Quaternion.identity);
        lastTime = currentTime;

        if(currentTime - lastTime >= timeInterval)
        {
            spawnPos1 = new Vector3(player.changeLane[laneChoice2].position.x, player.transform.position.y, player.transform.position.z + spawnOffset);
            hazard = Instantiate(hazards[hazardChoice], spawnPos1, Quaternion.identity);

        }
        else
        {
            currentTime = 0f;
        }

        //hide Prefab
        //if ((hazard.transform.position.z > cam.transform.position.z)
        //   && (hazard.transform.position.z < player.transform.position.z))
        //{
        //    hazard.SetActive(false);
        //}
        //else
        //{
        //    hazard.SetActive(true);
        //}

    }

    //void hideHazard()
    //{
    //    //Debug.Log("camPos");
    //    //Debug.Log(cam.transform.position.z);
    //    //Debug.Log("playerPos");
    //    //Debug.Log(player.transform.position.z);
    //    //(gameObject.transform.position.z < playerPos.position.z)
    //    if ((hazard.transform.position.z > cam.transform.position.z)
    //        && (hazard.transform.position.z < player.transform.position.z)) {
    //        hazard.SetActive(false);
    //    }
    //    else{
    //        hazard.SetActive(true);
    //    }
    //}
 }