using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;


public class MagicBookScript : MonoBehaviour
{
    private bool m_bisClosed;

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
        m_BookClosed.SetActive(true);
        m_bisClosed = true;
        m_PageScript = m_Page.GetComponent<PageScript>();
        m_PageScript.ReadyNextPage += TurnPage;
        
        
    }
    private void OnDestroy()
    {
        m_PageScript.ReadyNextPage -= TurnPage;
    }
   private void ClearPage()
    {
        m_PageScript.ClearPage();
    }
    public void OpenBook()
    {
        //AkSoundEngine.PostEvent("Book_open", gameObject);
        m_BookClosed.SetActive(false);
        m_BookOpen.SetActive(true);
        m_bisClosed = false;
        TurnPage();
    }
    public void CloseBook()
    {
        //AkSoundEngine.PostEvent("Book_close", gameObject);
        m_BookClosed.SetActive(true);
        m_BookOpen.SetActive(false);
        m_bisClosed = true;
        m_PageScript.ClearPage();
    }
    public void TurnPage()
    {
        ClearPage();
        //AkSoundEngine.PostEvent("Book_turn_page", gameObject);
        if (!m_bisClosed)
        {
            StartCoroutine(WaitTurnAnimation(1.0f));
        }
    }
    IEnumerator WaitTurnAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //get a list with the spellvariants to put on the page
        GameObject[] selectedSpellVariants = GetSpellVariants();
        m_PageScript.NextPage(selectedSpellVariants);
    }


    GameObject[] GetSpellVariants()
    {
        //select between 3 and 4 of the first spells in shuffledList
        int numberOfSpells = Random.Range(2, 4); //[2,3]
        int numberOfSpellcardVariants = Random.Range(4, 6);//[4,5]
        //Debug.Log("Creating " + numberOfSpellcardVariants + " spell card variants");

        GameObject[] shuffledSpellList = (GameObject[])m_Spells.Clone();
        GameObject[] selectedSpellsList = new GameObject[numberOfSpells];
        GameObject[] selectedSpellsVariantsList = new GameObject[numberOfSpellcardVariants];
        if (!m_PageScript)
        {
            Debug.Log("Book has not found PageScript");
        }

        m_PageScript.ShuffleList(shuffledSpellList);

        for (int i = 0; i < numberOfSpells; i++)
        {
            //select the first 2 or 3 spells from the shuffled spell list
            selectedSpellsList[i] = shuffledSpellList[i];
            //selectedSpellsList[i] = m_Spells[Random.Range(0, m_Spells.Length - 1)];
        }
        for (int i = 0; i < numberOfSpellcardVariants; i++)//create a list with 4-5 spell variants on a page
        {
            if (i < numberOfSpells)
            {
                selectedSpellsVariantsList[i] = selectedSpellsList[i];
            }
            else
            {
                //sicherstellen dass es von dem unique element mindestens zwei variants gibt damit man nie einfach durch farbe entscheiden kann
                if (i == numberOfSpells)
                {
                    selectedSpellsVariantsList[i] = selectedSpellsList[0];
                }
                else
                {
                    //ansonsten einen random spell aus selected spell list hinzufügen
                    selectedSpellsVariantsList[i] = selectedSpellsList[Random.Range(0, numberOfSpells)];//not certain if all spells in selected spell list will be chosen
                }
                
            }
            //Debug.Log("SpellVarians[" + i + "]= " + selectedSpellsVariantsList[i].name);
        }
        return selectedSpellsVariantsList;
    }

   
}
