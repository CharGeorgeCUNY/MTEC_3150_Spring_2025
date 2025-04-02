using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float healthRegen = 0.5f;

    private float damageTimer = 0f;
    private float regenDelay = 3f;

    void Update()
    {
        damageTimer += Time.deltaTime;

        if (damageTimer >= regenDelay && health < 100f)
        {
            RegenHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        damageTimer = 0f;

        health -= damage;
        if (health <= 0f)
        {
            SceneManager.LoadScene("DeathScene");
            Destroy(gameObject);
        }
    }


    private void RegenHealth()
    {
        health += healthRegen * Time.deltaTime;
        health = Mathf.Clamp(health, 0f, 100f);
    }
}
