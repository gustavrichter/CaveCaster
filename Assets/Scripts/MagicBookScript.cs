using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBookScript : MonoBehaviour
{
    private bool m_bisClosed = true;

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
        m_ClosedImage.SetActive(true);
        m_OpenImage.SetActive(false);
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
            GameObject[] shuffledSpellList = m_Spells;
            GameObject[] selectedSpellsList = null;
            m_PageScript.ShuffleList(shuffledSpellList);

            int r = Random.Range(2, 4);
            for (int i = 0 ; i < r; i++)
            {
                selectedSpellsList[i] = shuffledSpellList[i];
            }
            m_PageScript.NextPage(selectedSpellsList);

        }

    }
}
