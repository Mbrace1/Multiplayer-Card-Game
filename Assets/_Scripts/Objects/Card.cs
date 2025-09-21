using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

// mouse on enter, highlight rdy to move
// mouse on exit, highlight disable, no more move
// mouse on down, start the move
// mouse on release, drop into pos like column or hand, or go back to og pos
// mouse on click (show card details larger)

//script controls how the card interacts with the mouse and therefore moving around, dropping on board, etc


public class Card : MonoBehaviour,
    IDragHandler,
    IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerUpHandler, IPointerDownHandler
{
    // these are signals to be sent out to the game when the mouse interacts with the card
    [Header("Events")]
    [HideInInspector] public UnityEvent<Card> PointerEnterEvent;
    [HideInInspector] public UnityEvent<Card> PointerExitEvent;
    [HideInInspector] public UnityEvent<Card, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<Card> PointerDownEvent;
    [HideInInspector] public UnityEvent<Card> BeginDragEvent;
    [HideInInspector] public UnityEvent<Card> EndDragEvent;
    [HideInInspector] public UnityEvent<Card, bool> SelectEvent;


    // these are used for raycast checks involving the mouse hitting the card
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private RectTransform rectTransform;


    // card states
    [Header("States")]
    public bool isHovering;
    public bool isDragging;
    public bool selected;
    
    public bool canDrag = true;

    public CardData cardData;

    public Image cardImage;
    public TMP_Text cardScore;

    // temp data
    private int boardColumn = -1;
    private int row = -1;
    private int col = -1;
    private int handPos = -1;

    private Vector3 offset;
    [SerializeField] private float moveSpeedLimit = 50;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetBoardPosition(int rowId, int colId)
    {
        row = rowId;
        col = colId;

        // prevent further drag and drop as its been placed already
        canDrag = false;
    }

    public void SetCardData(CardData data)
    {
        cardData = data;
        cardImage.sprite = cardData.sprite;
        cardScore.text = cardData.score.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        BeginDragEvent.Invoke(this);
        isDragging = true;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
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