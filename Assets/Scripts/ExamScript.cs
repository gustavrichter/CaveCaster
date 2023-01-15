using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamScript : MonoBehaviour
{

    public MagicBookScript m_magicBookScript;
    public PageScript m_pageScript;
    public PlayerScript m_playerScript;
    public CaveScript m_caveScript;

    //private int m_gameCount;
    private void Start()
    {
        //m_gameCount = 0;
        m_magicBookScript = GameObject.FindGameObjectWithTag("MagicBook").GetComponent<MagicBookScript>();
        m_pageScript = m_magicBookScript.getPageScript();
        m_playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        m_caveScript = GameObject.FindGameObjectWithTag("Cave").GetComponent<CaveScript>();

        if (!m_magicBookScript)
        {
            Debug.Log("MagicBookScript not found");
        }
        if (!m_pageScript)
        {
            Debug.Log("PageScript not found");
        }
        if (!m_playerScript)
        {
            Debug.Log("PlayerScript not found");
        }
        if (!m_caveScript)
        {
            Debug.Log("CaveScript not found");
        }

        
    }

}
