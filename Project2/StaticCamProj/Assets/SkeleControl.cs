using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleControl : MonoBehaviour
{
    Animator ani;
    public float direction;
    public float turn;
    public bool slash, staggered, dead;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        direction = GetComponentInParent<Enemy1>().moveInput;
        turn = GetComponentInParent<Enemy1>().turnInput;
        slash = GetComponentInParent<Enemy1>().slash;
        staggered = GetComponentInParent<Enemy1>().hit;
        dead = GetComponentInParent<Enemy1>().death;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ani = GetComponent<Animator>();
        direction = GetComponentInParent<Enemy1>().moveInput;
        turn = GetComponentInParent<Enemy1>().turnInput;
        slash = GetComponentInParent<Enemy1>().slash;
        staggered = GetComponentInParent<Enemy1>().hit;
        dead = GetComponentInParent<Enemy1>().death;
        ani.SetFloat("FB", direction);
        ani.SetFloat("MT", turn);
        ani.SetBool("Slash", slash);
        ani.SetBool("Hit", staggered);
        ani.SetBool("Death", dead);
        this.transform.localPosition = new Vector3(0, -.5f, 0);
        if (dead)
        {
            this.transform.localPosition = new Vector3(0, -1f, 0);
        }
        
    }
}
