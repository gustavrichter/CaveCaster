using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragOnEnemyFire : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform myPosition;
    CanvasGroup myCanvasGroup;
    SpellScript mySpellScript;
   
    private void Awake()
    {
        //string spellName = GetComponentInParent<Transform>().name;
        string spellName = GetComponentInParent<Transform>().name;
        myPosition = gameObject.GetComponentInParent(typeof(RectTransform)) as RectTransform;
        myCanvasGroup = GetComponent<CanvasGroup>();
        mySpellScript = GetComponent<SpellScript>();
        if (!myCanvasGroup)
            Debug.Log(spellName + " canvGroup not found");
        if (!myPosition)
            Debug.Log(spellName + " rectTransform not found");
        if (!mySpellScript)
        {
            Debug.Log(spellName + "SpellScript not found");
        }


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
        //Debug.Log("EndDrag");
        if (mySpellScript.bunique)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit)
            {
                //EnemySlimeScript enemyscr = rayHit.transform.gameObject.GetComponent<EnemySlimeScript>();
                EnemyScript enemyScript = rayHit.transform.gameObject.GetComponent<EnemyScript>();
                if (!enemyScript)
                    Debug.Log("enemyscr not found");
                else
                {
                    enemyScript.TakeDamage(mySpellScript.getDamage(), mySpellScript.getElement());
                }

               
            }
            else
            {
                Debug.Log("No target hit");
            }
        }

        
        myCanvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");
    }
  
}

