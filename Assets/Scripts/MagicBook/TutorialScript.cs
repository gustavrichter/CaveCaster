using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject[] m_tutorialTexts = new GameObject[3];
    [SerializeField]
    MagicBookScript m_magicBookScript;
    [SerializeField]
    CaveScript m_caveScript;
    //List<GameObject> m_tutorialTexts = new List<GameObject>();

    //List<TextMeshProUGUI> m_textMessages = new List<TextMeshProUGUI>();

    private int m_pageIndex;

    private void Awake()
    {
        m_pageIndex = 0;
        for (int i = 0; i < m_tutorialTexts.Length; i++)
        {
            //Debug.Log("Setting texts inactive");
            m_tutorialTexts[i].SetActive(false);
        }
        StartTutorial();
    }
    
    public void StartTutorial()
    {
        //m_magicBookScript.m_BookClosed.SetActive(false);
        //m_magicBookScript.m_BookPause.SetActive(false);
        //Debug.Log("Setting start text active");
        m_tutorialTexts[m_pageIndex].SetActive(true);
    }

    public void NextPage()
    {
        m_tutorialTexts[m_pageIndex].SetActive(false);
        m_pageIndex++;
        m_tutorialTexts[m_pageIndex].SetActive(true);
    }

    public void DoPracticeFight()
    {
        //Debug.Log("DoingPracticeFight");
        //m_tutorialTexts[m_pageIndex].SetActive(false);
        m_caveScript.SpawnEnemyTutorial();
        m_magicBookScript.SetUseInk();
        EndTutorial();
    }

    public void EndTutorial()
    {
        gameObject.SetActive(false);
        GameKnowledge.m_bplayTutorial = false;
    }
}
