using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBookScript : MonoBehaviour
{
    private bool m_bisClosed = false;

    public GameObject m_OpenImage;
    public GameObject m_ClosedImage;

    [SerializeField]
    private GameObject[] m_Spells;//size=4
    

    [SerializeField]
    private GameObject m_Page;
    private PageScript m_PageScript;


    // Start is called before the first frame update
    void Start()
    {
        if(m_bisClosed)
            m_ClosedImage.SetActive(true);
        else
            m_OpenImage.SetActive(true);

        m_PageScript = m_Page.GetComponent<PageScript>();
    }

    // Update is called once per frame
    void Update()
    {
    

    }
    public void OpenBook()
    {

        if (m_bisClosed)
        {
            m_ClosedImage.SetActive(false);
            m_OpenImage.SetActive(true);
            m_bisClosed = false;
        }
        else
        {
            m_ClosedImage.SetActive(true);
            m_OpenImage.SetActive(false);
            m_bisClosed = true;
        }
    }
    public void TurnPage()
    {
        if (!m_bisClosed)
        {
            //select between 3 and 4 of the first spells in shuffledList
            int numberOfSpells = Random.Range(2, 5); //[2,4]

            GameObject[] shuffledSpellList = (GameObject[]) m_Spells.Clone();
            GameObject[] selectedSpellsList = new GameObject[numberOfSpells];
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
