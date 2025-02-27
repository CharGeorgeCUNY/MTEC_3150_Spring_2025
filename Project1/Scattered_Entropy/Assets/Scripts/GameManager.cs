using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI DialogueText;
    // public string 
    public int MaxHealth;
    public string MerchantDialogue;

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

    public string ShowDialogue(bool IsDialogue) {
        if (IsDialogue) {
            MerchantDialogue = "Welcome to this game";
            string Dialogue = "";

            Dialogue += (MerchantDialogue);
            DialogueText.SetText(Dialogue);
            
        }

        return "";
    }
}
