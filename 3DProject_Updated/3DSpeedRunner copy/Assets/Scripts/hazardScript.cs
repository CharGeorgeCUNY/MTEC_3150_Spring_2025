using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardScript : MonoBehaviour
{
    GameObject player;
    Camera cam;

    //movement
    Rigidbody rbody;
    float speed = 11.0f;

    //don't enter the camera
    float camOffset = 1.5f;

    //collision & explosion
    public GameObject explosion;

    void Start()
    {
        cam = Camera.main;
        player = GameObject.Find("Player");
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cam.transform.position.z);
        Debug.Log("camPos");
        Debug.Log(player.transform.position.z);
        Debug.Log("playerPos");

        rbody.velocity = -(Vector3.forward) * speed;

        if (gameObject.transform.position.z < cam.transform.position.z + camOffset)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
