using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

// mouse on enter, highlight rdy to move
// mouse on exit, highlight disable, no more move
// mouse on down, start the move
// mouse on release, drop into pos like column or hand, or go back to og pos
// mouse on click (show card details larger)

//script controls how the card interacts with the mouse and therefore moving around, dropping on board, etc


public class CardObject : MonoBehaviour,
    IDragHandler,
    IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerUpHandler, IPointerDownHandler
{
    // These are signals to be sent out to the game when the mouse interacts with the card
    [Header("Events")]
    [HideInInspector] public UnityEvent<CardObject> PointerEnterEvent;
    [HideInInspector] public UnityEvent<CardObject> PointerExitEvent;
    [HideInInspector] public UnityEvent<CardObject, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<CardObject> PointerDownEvent;
    [HideInInspector] public UnityEvent<CardObject> BeginDragEvent;
    [HideInInspector] public UnityEvent<CardObject> EndDragEvent;
    [HideInInspector] public UnityEvent<CardObject, bool> SelectEvent;


    // these are used for raycast checks involving the mouse hitting the card
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private RectTransform rectTransform;


    // card states
    [Header("States")]
    public bool isHovering;
    public bool isDragging;
    public bool selected;


    private Vector3 offset;
    [SerializeField] private float moveSpeedLimit = 50;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);
        isDragging = true;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);
        isDragging = false;

        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnterEvent.Invoke(this);
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExitEvent.Invoke(this);
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

}