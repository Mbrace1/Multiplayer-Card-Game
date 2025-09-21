using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    public TMP_Text playerOneScoreText;
    public TMP_Text playerTwoScoreText;

    public int columnId;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    private int minPoints = 0;
    private int maxPoints = 999;

    private List<Card> playerOneCards;
    private List<Card> playerTwoCards;

    public List<CardDropArea> dropAreas;



    void Start()
    {
        foreach (CardDropArea area in dropAreas)
        {
            area.CardAddedEvent.AddListener(OnCardAddedToArea);
        }
    }

    private void OnCardAddedToArea(Card card, GameManager.Player player)
    {
        UpdatePlayerScore(player, card.cardData.score);
    }

    // TODO remove card
    // TODO move card


    public void UpdatePlayerScore(GameManager.Player player, int points)
    {
        Debug.Log(player);
        //TODO tween
        if (GameManager.Player.Player1 == player) {
            playerOneScore += points;
            playerOneScore = Mathf.Clamp(playerOneScore, minPoints, maxPoints);
            // update ui
            playerOneScoreText.text = playerOneScore.ToString();
            Debug.Log(playerOneScore);
        }
        if (GameManager.Player.Player2 == player) {
            playerTwoScore += points;
            playerTwoScore = Mathf.Clamp(playerTwoScore, minPoints, maxPoints);
            // update ui
            playerOneScoreText.text = playerTwoScore.ToString();
            Debug.Log(playerTwoScore);
        }
        Debug.Log(points);
    }

    public int GetPlayerScore(GameManager.Player player) {
        if (GameManager.Player.Player1 == player) {
            return playerOneScore;
        }
        if (GameManager.Player.Player2 == player) {
            return playerTwoScore;
        }
        return 0;
    }
}
