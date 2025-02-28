using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Transform playerpos; // Assign the player GameObject in the inspector
    public float chaseRange = 10f; // Distance at which the enemy starts chasing
    public float moveSpeed = 0;  // Speed of the enemy
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        moveSpeed = Random.Range(3f, 4f);
        playerpos = player.transform;

    }
    void Update()
    {
        if (playerpos != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerpos.position);

            if (distanceToPlayer <= chaseRange)
            {
                ChasePlayer();
            }
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerpos.position, moveSpeed * Time.deltaTime);
        //transform.LookAt(playerpos); // Make the enemy face the player
    }
}
