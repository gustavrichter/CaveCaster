using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragOnEnemy : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform myPosition;
    Vector2 posAnchoredPosition;
    CanvasGroup myCanvasGroup;
    Canvas myCanvas;
    PointerEventData ped;
    public Action SpellCardDropped = delegate { };//will always be called, that way: won't give nullreference exception when no methods are subscribed
    public Action SpellCardDragged = delegate { };

    private void Awake()
    {
        myPosition = GetComponent<RectTransform>();
        myCanvasGroup = GetComponent<CanvasGroup>();
        myCanvas = GetComponentInParent<Canvas>();
        if (!myCanvas)
        {
            Debug.Log("canvas not found");
        }
    }
   void Start()
    {
        posAnchoredPosition = myPosition.anchoredPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
        myCanvasGroup.blocksRaycasts = false;
        SpellCardDragged();


    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //.anchoredPosition += eventData.delta / myPosition.lossyScale; //scaling it correctly with lossyScale
        myPosition.anchoredPosition += eventData.delta / myCanvas.scaleFactor; //scaling it correctly with lossyScale
        myCanvasGroup.blocksRaycasts = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + "SpellcardDropped");
        SpellCardDropped();

    }
    public void ResetPosition()
    {
        //Debug.Log("Resetting Position");
        myPosition.anchoredPosition = posAnchoredPosition;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");

    }

}

