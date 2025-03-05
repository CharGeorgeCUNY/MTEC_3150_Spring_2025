using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentAttack;
    float currentProjectileSpeed;

    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentAttack = characterData.Attack;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    void Update()
    {
        if (invincibilityTimer > 0) { invincibilityTimer -= Time.deltaTime; }
        else if (isInvincible) { isInvincible = false; }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth < 0)
            {

                Kill();
            }
        }
    }

    public void Kill()
    {
        
    }

}
