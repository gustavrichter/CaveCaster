using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnEnemy : MonoBehaviour, IDropHandler
{
   

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

    }
}
