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
    public AudioClip Audio;

    void Start()
    {
        StartTime = Time.realtimeSinceStartup;
        Vector2 PositionVector = GameObject.FindObjectOfType<Ship>().transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(PositionVector.normalized * InitialForce);
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
        if(collision.gameObject.GetComponent<Ship>() != null)
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
        AudioSource.PlayClipAtPoint(Audio,Vector3.zero);
        Destroy(this.gameObject);
    }
    public void onDestroyTime()
    {
        Destroy(this.gameObject);
    }

}
