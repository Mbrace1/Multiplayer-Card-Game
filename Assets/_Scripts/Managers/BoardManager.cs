using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//stores and updates the board data like card positions and points
public class BoardManager : MonoBehaviour
{

    [Header("Board Cols")]
    public ColumnManager[] cols;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // updates the col score for x player DO NOT USE ATM, ColumnManager instead
    public void UpdatePlayerColumnScore(GameManager.Player player, int column, int points)
    {
        cols[column].GetComponent<ColumnManager>().UpdatePlayerScore(player, points);
    }

    // gets the col score for x player DO NOT USE ATM, ColumnManager instead
    public int GetPlayerColumnScore(GameManager.Player player, int column)
    {
        return cols[column].GetComponent<ColumnManager>().GetPlayerScore(player);
    }
}
