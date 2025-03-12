using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : iDamageable
{
    // Start is called before the first frame update
    private float StartTime;
    [SerializeField]
    private float DeathTime = 6f;
    public float InitialForce=100f;
    SpriteRenderer sr;

    void Start()
    {
        StartTime = Time.realtimeSinceStartup;
        Vector2 PositionVector = GameObject.FindObjectOfType<Ship>().transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(PositionVector.normalized * InitialForce);

        //some edits
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup - StartTime > DeathTime)
        {
            onDestroyTime();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ship>() != null)
        {
            collision.gameObject.GetComponent<Ship>().DoDamage();
        }
        
    }


    public override void DoDamage()
    {
        onDestroyDamage();
    }

    public void onDestroyDamage()
    {
        StartCoroutine(BlinkRed());
        //Destroy(this.gameObject);
    }
    public void onDestroyTime()
    {
        Destroy(this.gameObject);
    }

    public IEnumerator BlinkRed()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);

        //canTakeDamage = false;
        sr.color = Color.red;
        yield return delay;
        sr.color = Color.white;
        yield return delay;
        sr.color = Color.red;
        yield return delay;
        sr.color = Color.white;
        yield return delay;
        sr.color = Color.red;
        yield return delay;
        sr.color = Color.white;
        yield return delay;
        Destroy(this.gameObject);
        //canTakeDamage = true;
    }

}
