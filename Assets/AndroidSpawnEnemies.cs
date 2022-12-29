using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidSpawnEnemies : MonoBehaviour, IPointerDownHandler
{
    GameObject m_Cave;
    CaveScript m_caveScript;
    void Awake()
    {
        m_Cave = GameObject.Find("CaveMaxi");
        //m_Cave = GameObject.Find("Cave");
        m_caveScript = m_Cave.GetComponent<CaveScript>();
        if (!m_caveScript)
        {
            Debug.Log("cavescript not found");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_caveScript.SpawnEnemy();
    }
}
