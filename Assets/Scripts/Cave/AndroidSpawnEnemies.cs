using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidSpawnEnemies : MonoBehaviour, IPointerDownHandler
{
    CaveScript m_caveScript;
    void Awake()
    {
        m_caveScript = GameObject.FindGameObjectWithTag("Cave").GetComponent<CaveScript>();
        if (!m_caveScript)
        {
            Debug.Log("cavescript not found");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //m_caveScript.StartCoroutine("DelayedSpawnEnemies", 3.0f);
        if (!(m_caveScript.getEnemies().Count > 0))//if no enemies already exist
        {
            m_caveScript.SpawnEnemy();

        }
    }
}
