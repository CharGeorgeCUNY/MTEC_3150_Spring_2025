using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControl : MonoBehaviour
{
  
    Animator ani;
    public float direction;
    public float turn;
    public bool slash, staggered, dead;
    // Update is called once per frame
    private void Start()
    {
        
        ani = GetComponent<Animator>();
        direction = GetComponentInParent<Player>().moveInput;
        turn = GetComponentInParent<Player>().turnInput;
        slash = GetComponentInParent<Player>().slash;
        staggered = GetComponentInParent<Player>().hit;
        dead = GetComponentInParent<Player>().death;
    }
    void Update()
    {

        


        direction = GetComponentInParent<Player>().moveInput;
        turn = GetComponentInParent<Player>().turnInput;
        slash = GetComponentInParent<Player>().slash;
        staggered = GetComponentInParent<Player>().hit;
        dead = GetComponentInParent<Player>().death;

        ani.SetFloat("FB", direction);
        ani.SetFloat("MT", turn);
        ani.SetBool("Slash", slash);
        ani.SetBool("Hit", staggered);
        ani.SetBool("Death", dead);

        //ani.SetFloat("MT", direction);
        //this.transform.position = Player.transform.position;

    }
    private void FixedUpdate()
    {
        
       // this.transform.localEulerAngles = Vector3.zero;
        this.transform.localPosition = new Vector3(-0.15f, -1, .2f);
        if (dead)
        {
            this.transform.localPosition = new Vector3(-0.15f, -1.5f, .2f);
        }

    }
}
