using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// will hold all the players cards and current decks
// can unlock new cards and should connect to the card database (through aws)
public class CardManager : MonoBehaviour
{
    public List<Card> playerCards;

    public Deck currentDeck;

    public List<Deck> decks;


}
