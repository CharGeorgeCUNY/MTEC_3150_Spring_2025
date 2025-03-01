using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades,
    Wildcard
}

[CreateAssetMenu(fileName = "CardData", menuName = "Card Game/Card Data")]
public class Card : MonoBehaviour
{
    public CardSuit suit;
    public int rank;
    public Sprite miniCardSprite;
    public Sprite cardSprite;

    void Start()
    {
        if (cardSprite != null) { return; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
