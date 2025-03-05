using UnityEngine;

public enum CardSuit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades,
    Wildcard
}

// Use ScriptableObject for card data
[CreateAssetMenu(fileName = "NewCardData", menuName = "ScriptableObjects/Cards")]
public class CardScriptableObject : ScriptableObject
{
    public CardSuit suit;
    public int rank;
    public Sprite miniCardSprite;
    public Sprite cardSprite;

void Start()
    {
        if (cardSprite != null) { return; }
    }

    void Update()
    {
        
    }
}
