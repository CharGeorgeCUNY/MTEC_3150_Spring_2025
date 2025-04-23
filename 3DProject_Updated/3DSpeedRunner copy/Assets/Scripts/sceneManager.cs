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
    float spawnOffset = 10.0f;

    float currentTime = 0f;
    float lastTime = 0f;
    float timeInterval = 1.5f;

    //playerMovement script referencing
    public playerMovement player;
    //audio
    AudioSource sound;
    public AudioClip warningSound;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 4.0f, timeInterval);
        sound = gameObject.GetComponent<AudioSource>();
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

        spawnPos = new Vector3(player.changeLane[laneChoice1].position.x, player.transform.position.y, player.transform.position.z + spawnOffset);
        StartCoroutine(audioPlayer());
        Instantiate(hazards[hazardChoice], spawnPos, Quaternion.Euler(new Vector3(0, 180, 0)));
        lastTime = currentTime;

        if(currentTime - lastTime >= timeInterval)
        {
            spawnPos1 = new Vector3(player.changeLane[laneChoice2].position.x, player.transform.position.y, player.transform.position.z + spawnOffset);
            Debug.Log("play warning");
            StartCoroutine(audioPlayer());
            Instantiate(hazards[hazardChoice], spawnPos1, Quaternion.Euler(new Vector3(0, 180, 0)));

        }
        else
        {
            currentTime = 0f;
        }

    }

    public IEnumerator audioPlayer()
    {

        WaitForSeconds delay = new WaitForSeconds(1.0f);
        sound.PlayOneShot(warningSound, 2.8f);
        yield return delay;

    }


}