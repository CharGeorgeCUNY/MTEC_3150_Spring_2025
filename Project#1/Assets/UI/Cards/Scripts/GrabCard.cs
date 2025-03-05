using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCard : MonoBehaviour
{
    // Reference to the Card script/data you'd like to add to the player's hand
    public CardScriptableObject cardData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Card picked up by the player!");

            // 1) Find or reference your GameManager (or any script that handles the player's cards)
            GameManager gm = FindObjectOfType<GameManager>();

            // 2) Add this card's data to the player's hand
            gm.AddCard(cardData);

            // 3) Destroy the pickup object
            Destroy(gameObject);
        }
    }
}