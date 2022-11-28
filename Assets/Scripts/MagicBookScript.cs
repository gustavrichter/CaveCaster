using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBookScript : MonoBehaviour
{
    private bool m_bisClosed = false;

    public GameObject m_BookOpen;
    public GameObject m_BookClosed;

    [SerializeField]
    private GameObject[] m_Spells;//size=5
    

    [SerializeField]
    private GameObject m_Page;
    private PageScript m_PageScript;


    // Start is called before the first frame update
    void Awake()
    {
        
        m_BookOpen.SetActive(false);
        m_BookClosed.SetActive(false);
        m_bisClosed = true;

        OpenBook();

        m_PageScript = m_Page.GetComponent<PageScript>();
        m_Page.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
    

    }
   
    public void OpenBook()
    {

        if (m_bisClosed)
        {
            m_BookClosed.SetActive(false);
            m_BookOpen.SetActive(true);
            m_bisClosed = false;
        }
        else
        {
            m_BookClosed.SetActive(true);
            m_BookOpen.SetActive(false);
            m_bisClosed = true;

        }
    }
    public void TurnPage()
    {
        if (!m_bisClosed)
        {
            //select between 3 and 4 of the first spells in shuffledList
            int numberOfSpells = Random.Range(3, 5); //[3,4]

            GameObject[] shuffledSpellList = (GameObject[]) m_Spells.Clone();
            GameObject[] selectedSpellsList = new GameObject[numberOfSpells];
            if (!m_PageScript)
            {
                Debug.Log("Book has not found PageScript");
            }
            m_PageScript.ShuffleList(m_Spells);
            m_PageScript.ShuffleList(shuffledSpellList);

            for (int i = 0 ; i < numberOfSpells; i++)
            {
                selectedSpellsList[i] = shuffledSpellList[i];
                //Debug.Log(selectedSpellsList[i].ToString());
            }
            
            m_PageScript.NextPage(selectedSpellsList);

        }

    }
}
