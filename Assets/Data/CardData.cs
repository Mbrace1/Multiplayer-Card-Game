using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewCardData",
    menuName = "Cards/Card Data",
    order = 0
)]

public class CardData : ScriptableObject
{
    public string cardName;
    public int score;
    public int cost;
    public Sprite sprite;
    public string description;
}
