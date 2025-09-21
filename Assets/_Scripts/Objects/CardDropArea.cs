using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDropArea : MonoBehaviour, IDropHandler
{
    // position in the column should be 1-9
    public int rowId;
    public int colId;
    public GameManager.Player playerArea;

    [System.Serializable]
    public class CardAddedEventType : UnityEvent<Card, GameManager.Player> { }
    public CardAddedEventType CardAddedEvent = new CardAddedEventType();

    // emit an event which the boardManager receives with card data and pos
    public void OnDrop(PointerEventData eventData)
    {
        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();

            draggedRectTransform.transform.position = transform.position;
            draggedRectTransform.localScale = Vector3.one;

            Card cardObj = draggedObject.GetComponent<Card>();
            cardObj.SetBoardPosition(rowId, colId);
            CardAddedEvent.Invoke(cardObj, playerArea);
        }
    }
}
