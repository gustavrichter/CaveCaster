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
    PointerEventData ped;
    public Action SpellCardDropped = delegate {};//will always be called, that way: won't give nullreference exception when no methods are subscribed

    private void Awake()
    {
        myPosition = GetComponent<RectTransform>();
        myCanvasGroup = GetComponent<CanvasGroup>();
    }
   void Start()
    {
        posAnchoredPosition = myPosition.anchoredPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
     
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
        myCanvasGroup.blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        myPosition.anchoredPosition += eventData.delta;// / myPosition.lossyScale; //scaling it correctly with lossyScale
       


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "SpellcardDropped");
        SpellCardDropped();

        myCanvasGroup.blocksRaycasts = true;
    }
    public void ResetPosition()
    {
        myPosition.anchoredPosition = posAnchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");

    }

}

