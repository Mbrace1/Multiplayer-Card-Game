using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class HandContainer : MonoBehaviour
{
    [SerializeField] private CardObject selectedCard;
    [SerializeReference] private CardObject hoveredCard;

    [SerializeField] private CardObject deck;

    public List<CardObject> cardsInHand;

    private int cardsToSpawn = 7;

    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    // will take cards from deck, need deck ref
    // add to cards in hand list
    // remove cards from hand list
    //listens to card events

    private void Start()
    {

        //ClearHand();

        //for (int i = 0; i < cardsToSpawn; i++)
        //{
        //    Instantiate(slotPrefab, transform);
        //}

        rect = GetComponent<RectTransform>();

        // add cards to hand list
        cardsInHand = GetComponentsInChildren<CardObject>().ToList();

        int cardCount = 0;


        // subscribe to the events in CardObject
        foreach (CardObject card in cardsInHand)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.name = cardCount.ToString();
            cardCount++;
        }
    }


    void CardPointerEnter(CardObject card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(CardObject card)
    {
        hoveredCard = null;
    }

    private void BeginDrag(CardObject card)
    {
        selectedCard = card;
    }


    void EndDrag(CardObject card)
    {
        if (selectedCard == null)
            return;

        rect.sizeDelta += Vector2.right;
        rect.sizeDelta -= Vector2.right;

        selectedCard = null;

    }


    void ClearHand()
    {
        // destroy every child under this transform
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // also clear your list
        cardsInHand.Clear();
    }

}
