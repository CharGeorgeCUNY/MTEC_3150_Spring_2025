using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    // public string 
    public int MaxHealth;
    public int PlayerDeath = 0;
    public string LosingWords;
    public TextMeshProUGUI DefeatedText;

    public string MerchantFirstDialo;
    public string MerchantSecondDialo;
    public string MerchantThirdDialo;
    public TextMeshProUGUI DialogueText;
    public float TextTime = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        MerchantFirstDialo = GetComponent<TextMeshProUGUI>().text;
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
                LosingWords = "You Lose!";
                string LoseText = "";

                LoseText += (LosingWords);
                DefeatedText.SetText(LoseText);
            }
        }

        return 0;
    }

    //get a max of 3 points to represent all three monsters to win game
    // public int IncrementScore() {

    // }

    public string StartDialogue() {
        StartCoroutine(DialogueCoRoutine(DialogueText));
        return "";
    }

    IEnumerator DialogueCoRoutine(bool IsDialogue) {
        while (true) {
            if (IsDialogue) {
                MerchantFirstDialo = "Welcome to this game.";
                MerchantSecondDialo = "Use WASD or the Arrow Keys to move.";
                MerchantThirdDialo = "Use M1 to defeat 3 monsters.";
            }

            yield return new WaitForSeconds(TextTime);
            string Dialogue1 = "";
            Dialogue1 += (MerchantFirstDialo);
            DialogueText.SetText(Dialogue1);
            yield return new WaitForSeconds(TextTime);
            string Dialogue2 = "";
            Dialogue2 += (MerchantSecondDialo);
            DialogueText.SetText(Dialogue2);
            yield return new WaitForSeconds(TextTime);
            string Dialogue3 = "";
            Dialogue3 += (MerchantThirdDialo);
            DialogueText.SetText(Dialogue3);
        }
    }
}
