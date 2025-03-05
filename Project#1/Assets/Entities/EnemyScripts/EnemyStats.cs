using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public AudioSource hitSound;
    public AudioSource deathSound;
    public EnemyScriptableObject enemyData;
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        hitSound.Play();
        currentHealth -= dmg;

        if (currentHealth <= 0) { Kill(); }
    }

    public void Kill()
    {
        deathSound.Play();
        Destroy(gameObject, deathSound.clip.length);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}
