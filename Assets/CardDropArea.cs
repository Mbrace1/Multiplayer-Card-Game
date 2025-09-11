using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropArea : MonoBehaviour, IDropHandler
{
    // col id
    // pos id
    // card id

    // will need to remove card from parent slot and attach it under drop area

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped on Slot");

        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();

            // Parent it to this slot, reset local transform
            draggedRectTransform.SetParent(transform, false); // false = match slot space
            draggedRectTransform.anchoredPosition = Vector2.zero;
            draggedRectTransform.localScale = Vector3.one; // ensure correct scale
        }
    }
}
