using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Now store CardScriptableObject instead of a MonoBehaviour
    public List<CardScriptableObject> playerCards = new List<CardScriptableObject>();

    // Updated signature to accept CardScriptableObject
    public void AddCard(CardScriptableObject newCard)
    {
        if (playerCards.Count < 5)
        {
            playerCards.Add(newCard);
        }

        if (playerCards.Count == 5)
        {
            string bestHand = EvaluateHand(playerCards);
            Debug.Log($"Best Hand: {bestHand}");
            // Apply the corresponding buff/effect here
        }
    }

    private string EvaluateHand(List<CardScriptableObject> cards)
    {
        Dictionary<int, int> rankCounts = new Dictionary<int, int>();
        Dictionary<CardSuit, int> suitCounts = new Dictionary<CardSuit, int> {
            { CardSuit.Hearts,   0 },
            { CardSuit.Diamonds, 0 },
            { CardSuit.Clubs,    0 },
            { CardSuit.Spades,   0 }
        };

        List<int> ranks = new List<int>();

        // Fill dictionaries
        foreach (CardScriptableObject c in cards)
        {
            // Count suits
            suitCounts[c.suit]++;

            // Count ranks
            if (!rankCounts.ContainsKey(c.rank))
            {
                rankCounts[c.rank] = 0;
            }
            rankCounts[c.rank]++;

            ranks.Add(c.rank);
        }

        ranks.Sort();

        bool isFlush = IsFlush(suitCounts);
        bool isStraight = IsStraight(ranks, out bool isAceLow);
        bool isRoyal = (isFlush && isStraight && !isAceLow && IsRoyalFlush(ranks));

        // Evaluate combos from strongest to weakest
        if (isRoyal) return "Royal Flush";
        if (isFlush && isStraight) return "Straight Flush";
        if (IsFourOfAKind(rankCounts)) return "Four of a Kind";
        if (IsFullHouse(rankCounts)) return "Full House";
        if (isFlush) return "Flush";
        if (isStraight) return "Straight";
        if (IsThreeOfAKind(rankCounts)) return "Three of a Kind";
        if (IsTwoPair(rankCounts)) return "Two Pair";
        if (IsOnePair(rankCounts)) return "One Pair";
        return "High Card";
    }

    private bool IsFlush(Dictionary<CardSuit, int> suitCounts)
    {
        foreach (var kvp in suitCounts)
        {
            if (kvp.Value == 5)
                return true;
        }
        return false;
    }

    private bool IsStraight(List<int> sortedRanks, out bool isAceLow)
    {
        isAceLow = false;

        if (IsConsecutive(sortedRanks))
        {
            return true;
        }

        // Special case: Ace can act as '1' in "A 2 3 4 5"
        if (sortedRanks[0] == 2 &&
            sortedRanks[1] == 3 &&
            sortedRanks[2] == 4 &&
            sortedRanks[3] == 5 &&
            sortedRanks[4] == 14)
        {
            isAceLow = true;
            return true;
        }

        return false;
    }

    private bool IsConsecutive(List<int> sortedRanks)
    {
        for (int i = 1; i < sortedRanks.Count; i++)
        {
            // If any gap is bigger than 1, no consecutive
            if (sortedRanks[i] != sortedRanks[i - 1] + 1)
                return false;
        }
        return true;
    }

    private bool IsRoyalFlush(List<int> sortedRanks)
    {
        return (sortedRanks.Count == 5 &&
                sortedRanks[0] == 10 &&
                sortedRanks[1] == 11 &&
                sortedRanks[2] == 12 &&
                sortedRanks[3] == 13 &&
                sortedRanks[4] == 14);
    }

    private bool IsFourOfAKind(Dictionary<int, int> rankCounts)
    {
        foreach (var kvp in rankCounts)
        {
            if (kvp.Value == 4)
                return true;
        }
        return false;
    }

    private bool IsFullHouse(Dictionary<int, int> rankCounts)
    {
        bool hasThree = false;
        bool hasPair = false;

        foreach (var kvp in rankCounts)
        {
            if (kvp.Value == 3) hasThree = true;
            if (kvp.Value == 2) hasPair = true;
        }
        return (hasThree && hasPair);
    }

    private bool IsThreeOfAKind(Dictionary<int, int> rankCounts)
    {
        foreach (var kvp in rankCounts)
        {
            if (kvp.Value == 3)
                return true;
        }
        return false;
    }

    private bool IsTwoPair(Dictionary<int, int> rankCounts)
    {
        int pairCount = 0;
        foreach (var kvp in rankCounts)
        {
            if (kvp.Value == 2)
                pairCount++;
        }
        return (pairCount == 2);
    }

    private bool IsOnePair(Dictionary<int, int> rankCounts)
    {
        int pairCount = 0;
        foreach (var kvp in rankCounts)
        {
            if (kvp.Value == 2)
                pairCount++;
        }
        return (pairCount == 1);
    }

    // private void ApplyBuff(string bestHand)
    // {
    //     switch (bestHand)
    //     {
    //         case "Royal Flush":
    //             // big buff
    //             break;
    //         case "Straight Flush":
    //             // ...
    //             break;
    //         // etc.
    //     }
    // }
}
