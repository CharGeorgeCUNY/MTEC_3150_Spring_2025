using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float TimeSpawned;
    public float BulletTimeAlive = 2f;
    public float Speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        TimeSpawned = Time.timeSinceLevelLoad;
        GetComponent<Rigidbody2D>().AddForce(transform.up * Speed);
    }

    // Update is called once per frame
    void Update()
    {
       //f Debug.Log(string.Format("{0}, {1}, {2}", Time.timeSinceLevelLoad, TimeSpawned, BulletTimeAlive));
        if (Time.timeSinceLevelLoad - TimeSpawned > BulletTimeAlive)
        {
            DoDestroyTime();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision entered bullet");
        iDamageable testObj = collision.gameObject.GetComponent<iDamageable>();
        if(testObj != null)
        {
            testObj.DoDamage();
        }

        DoDestroyHit();
    }

    void DoDestroyTime()
    {
        Destroy(this.gameObject);
    }

    void DoDestroyHit()
    {
        Destroy(this.gameObject);
    }
}
