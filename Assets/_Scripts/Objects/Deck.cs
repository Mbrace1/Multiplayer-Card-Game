using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deck will have funcs to shuffle, draw
/// </summary>
public class Deck : MonoBehaviour
{
    public List<CardData> cards = new List<CardData>();


    private void Start()
    {
        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        var count = cards.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = cards[i];
            cards[i] = cards[r];
            cards[r] = tmp;
        }
    }

    /// <summary>
    /// Draws the top cards from the deck. Returns null if empty.
    /// </summary>
    public List<CardData> DrawCard(int number)
    {
        List<CardData> drawnCards = new List<CardData>();

        // clamp count to the remaining cards
        int cardsToDraw = Mathf.Min(number, cards.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            CardData cardData = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            drawnCards.Add(cardData);
        }

        if (cardsToDraw < number)
        {
            Debug.LogWarning($"Requested {number} cards but only {cardsToDraw} were available.");
        }

        return drawnCards;
    }
}
