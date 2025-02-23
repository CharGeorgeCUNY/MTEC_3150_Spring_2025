using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public int MaxHealth;

    public int PlayerDeath = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // [when the player gets hit by the enemy] 
    // in the enemy's script call this function by 
    // using GameObject.FindObjectOfType 
    // <GameManager>().this function

    public int IncrementHealth(bool IsHealth) {
        if (IsHealth) {
            MaxHealth -= 1;
            string Health = "";

            // integrates MaxHealth as string before 
            // being set as a text in the editor
            Health += (MaxHealth);
            HealthText.SetText(Health);

            // what is outputted once MaxHeath reaches 0
            if (MaxHealth <= PlayerDeath) {
                Debug.Log("Merchant: You lose!");
            }
        }

        return 0;
    }
}
