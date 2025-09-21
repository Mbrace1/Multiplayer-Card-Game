using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using DG.Tweening;

/// <summary>
/// Hand has funcs to listen to mouse/card interaction, stores cards in hand and draws from deck
/// </summary>
public class HandContainer : MonoBehaviour
{
    [SerializeField] private Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private Deck deck;
    [SerializeField] private GameObject slotPrefab;

    public List<Card> cardsInHand;
    public int initialCardsToDraw = 3;
    private RectTransform rect;

    private void Start()
    {

        ClearHand();
        List<CardData> cardsData = deck.DrawCard(initialCardsToDraw);

        // create the hand slots for the cards in the world
        // (a parent for the cards to stay in for now and move back to on failed board placement)
        // attach the data to the card
        for (int i = 0; i < cardsData.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, transform);
            Card card = slot.GetComponentInChildren<Card>();
            card.SetCardData(cardsData[i]);
            cardsInHand.Add(card);

            // start tween with card at deck pos and no scale
            // TODO stagger the card tweens
            RectTransform cardRect = card.GetComponent<RectTransform>();
            RectTransform deckRect = deck.GetComponent<RectTransform>();
            RectTransform slotRect = slot.GetComponent<RectTransform>();

            //THIS IS OVERCOMPLEX NEEDS LOOKING INTO
            // start at deck position (converted to card's parent space)
            Vector3 deckLocalPos = cardRect.parent.InverseTransformPoint(deckRect.position);
            cardRect.localPosition = deckLocalPos;
            card.transform.localScale = Vector3.zero;

            // target position in local space of card's parent
            Vector3 slotLocalPos = cardRect.parent.InverseTransformPoint(slotRect.position);

            // tween to slot
            cardRect.DOScale(Vector3.one, 2f).SetEase(Ease.OutCubic);
            cardRect.DOLocalMove(slotLocalPos, 2f).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                // optional: do something after animation ends
            });
        }
         
        // why do I need rect?
        rect = GetComponent<RectTransform>();

        int cardCount = 0;
        // subscribe (listen) to the events in Card
        foreach (Card card in cardsInHand)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.name = cardCount.ToString();
            cardCount++;
        }
    }


    void CardPointerEnter(Card card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        hoveredCard = null;
    }

    private void BeginDrag(Card card)
    {
        selectedCard = card;
    }


    void EndDrag(Card card)
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
