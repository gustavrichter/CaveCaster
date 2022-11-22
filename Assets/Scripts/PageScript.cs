using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PageScript : MonoBehaviour
{

    
    [SerializeField]
    private GameObject[] m_SpellPositions; //size=18

    private List<GameObject> m_SpellsOnPage = new List<GameObject>(); //dynamic size
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage(GameObject[] spellVariants)
    {
        ClearPage();
        //debug
        GameObject obj = Instantiate(spellVariants[0]);
        //obj.transform.SetParent(this.transform);

        //shuffle transform list
        ShuffleList(m_SpellPositions);
        
        int IndexPositionList = 0;
   
        for (int i = 0; i < spellVariants.Length; i++)
        {
            int numberOfSpellcards = Random.Range(2, 5); //[2,4]
            int numberOfRunes = Random.Range(1, 4); //[1,3]

            //first Spell in spellVariants[] will be unique on the page
            if(i == 0)
                numberOfSpellcards = 1;

            Debug.Log(spellVariants[0].ToString() + ": numberofSpellcards = " + numberOfSpellcards + ". numberOfRunes = " + numberOfRunes);
            for (int j = 0; j < numberOfSpellcards; j++)
            {
                //Debug.Log("IndexPosiontList: " + IndexPositionList);

                //m_SpellsOnPage.Add(Instantiate(spellVariants[i], m_SpellPositions[IndexPositionList].transform));
                m_SpellsOnPage.Add(Instantiate(spellVariants[i]));
                m_SpellsOnPage[m_SpellsOnPage.Count - 1].SetActive(true);
                //m_SpellsOnPage[m_SpellsOnPage.Count - 1].transform.SetParent(this.transform);

                GameObject temp = m_SpellsOnPage[m_SpellsOnPage.Count - 1].transform.GetChild(0).transform.GetChild(0).gameObject;
                RectTransform recttrans = temp.GetComponent<RectTransform>();
                recttrans.anchoredPosition = m_SpellPositions[IndexPositionList].transform.position;
                //temp.transform.position = m_SpellPositions[IndexPositionList].transform.position;
                //Debug.Log(temp.ToString());

                SpellScript spell = m_SpellsOnPage[m_SpellsOnPage.Count - 1].GetComponent<SpellScript>();
                
                spell.DrawRunes(numberOfRunes);

                IndexPositionList++;
            }
        }
    }

    private void ClearPage()
    {
        if (m_SpellsOnPage.Count > 0)
        {
            for (int i = 0; i < m_SpellsOnPage.Count; i++)
            {
                //Debug.Log("destroying SpellsOnPage[" + i + "]");
                Destroy(m_SpellsOnPage[i]);
            }

        }
       
    }

    public void ShuffleList(GameObject[] List)
    {

        for (int i = 0; i < List.Length; i++)
        {
            GameObject temp = List[i];

            int r = Random.Range(0, List.Length-1);
            //r = Random.Range(i, List.Length-1);
            List[i] = List[r];
            List[r] = temp;
        }
    }
}
