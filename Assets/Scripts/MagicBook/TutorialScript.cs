using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject[] m_tutorialTexts = new GameObject[3];
    //List<GameObject> m_tutorialTexts = new List<GameObject>();

    //List<TextMeshProUGUI> m_textMessages = new List<TextMeshProUGUI>();

    private int m_pageCounter;

    private void Awake()
    {
        m_pageCounter = 0;
        for (int i = 0; i < m_tutorialTexts.Length; i++)
        {
            Debug.Log("Setting texts inactive");
            m_tutorialTexts[i].SetActive(false);
        }
    }
    
    public void StartTutorial()
    {
        for (int i = 0; i < m_tutorialTexts.Length; i++)
        {
            m_tutorialTexts[i].SetActive(false);
        }
        Debug.Log("Setting start text active");
        m_tutorialTexts[0].SetActive(true);
        m_pageCounter++;
    }

    public void NextPage()
    {
        if (m_pageCounter == m_tutorialTexts.Length)
        {
            gameObject.SetActive(false);
        }
        else
        {
            m_tutorialTexts[m_pageCounter - 1].SetActive(false);
            m_tutorialTexts[m_pageCounter].SetActive(true);
            m_pageCounter++;

        }
    }
}
