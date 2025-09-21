using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // this will be a state machine for the flow of game turns
    // will send api requests to the multiplayer side


    // game flow:
    // like snap cards have a cost
    // this means you dont have a deck full of insane cards like alexander the great etc
    // players draw 3 cards initially
    // mana cost == 1, mana goes up every turn, and cards cost mana to play
    // player one goes first and draws a card
    // player one plays cards equal to mana on any column
    // end turn
    // player two goes and does the same process
    // game ends at turn 10? highest points  win
    public static GameManager Instance { get; private set; }

    public enum Player
    {
        Player1,
        Player2
    }


    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Persist across scenes if needed
        DontDestroyOnLoad(gameObject);
    }
}
