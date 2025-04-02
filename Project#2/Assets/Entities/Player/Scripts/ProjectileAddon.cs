using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public AudioSource critSound;
    public int damage;
    [Tooltip("Chance (in %) to deal a critical hit (double damage).")]
    public int critRate = 2;

    private Rigidbody rb;
    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit) { return; }
        else { targetHit = true; }
            
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            bool isCrit = Random.Range(0, 100) < critRate;
            int finalDamage = isCrit ? damage * 4 : damage;

            if (isCrit)
            {
                Debug.Log("Critical hit! Damage dealt: " + finalDamage);
                critSound.Play();
            }

            enemy.TakeDamage(finalDamage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}