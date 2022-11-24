using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragOnEnemy : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform myPosition;
    CanvasGroup myCanvasGroup;
    Color rayColor;
    public Camera mainCamera;
    private void Awake()
    {
        myPosition = GetComponent<RectTransform>();
        myCanvasGroup = GetComponent<CanvasGroup>();
        rayColor = Color.red;
        if (!myCanvasGroup)
            Debug.Log("canvGroup not found");

    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
       

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
        myCanvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        myPosition.anchoredPosition += eventData.delta / myPosition.lossyScale; //scaling it correctly with lossyScale
       


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        Ray aray = new Ray(Input.mousePosition, this.transform.forward*30);
        RaycastHit hit;
        if (Physics.Raycast(aray, out hit))
        {
            Debug.Log("Hit Something");
        }
        myCanvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");
    }
    void Update()
    {
        //Vector3 screenpos = Input.mousePosition;
        //Vector3 screenpos = myPosition.lossyScale;
        Vector3 screenpos = myPosition.transform.position;
        
        screenpos.z = Camera.main.nearClipPlane + 1; ;
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(screenpos), this.transform.forward*20, rayColor);

    }
}

