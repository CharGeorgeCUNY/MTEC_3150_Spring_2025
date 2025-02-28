using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : iDamageable
{
    public GameObject explosionEffect;
    Transform playerpos; 
    public float chaseRange = 10f; 
    public float moveSpeed = 0;  
    GameObject player;
    GameObject manager;

    private void Start()
    {
        manager = GameObject.Find("GameBounds");
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
       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().DoDamage();
        }
    }


    public override void DoDamage()
    {
        var level = GameObject.Find("LevelDisplay");
        level.GetComponent<LevelDisplay>().AddDeaths();




        onDestroyDamage();
    }

    public void onDestroyDamage()
    {
        Instantiate(explosionEffect, transform.position + Vector3.forward * -10, Quaternion.identity);

        Destroy(this.gameObject);
    }
    //public void onDestroyTime()
    //{
    //    Destroy(this.gameObject);
    //}

}
