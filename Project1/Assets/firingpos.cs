using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firingpos : MonoBehaviour
{
    GameObject Player;
    Animator ani;
    Vector2 direction;
    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
        direction = Player.GetComponent<Player>().direction;
    }
    void Update()
    {
        direction = Player.GetComponent<Player>().direction;


        ani.SetFloat("MX", direction.x);

        ani.SetFloat("MY", direction.y);
        this.transform.position = Player.transform.position;
         
    }
}
