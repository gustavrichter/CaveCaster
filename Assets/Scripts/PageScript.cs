using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PageScript : MonoBehaviour
{

    
    [SerializeField] private GameObject[] m_SpellPositions; //size=12
    [SerializeField] private Canvas mainCanvas;

    public int numberOfSpellcards;
    public int maxNumberOfSpellcards;
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
        //shuffle transform list to place the spellcards randomly
        ShuffleList(m_SpellPositions);
        
        int IndexOnPositionList = 0;

        int maxNumberOfSpellcards = 12;
        //if (Random.Range(0, 2) == 1)
        //    maxNumberOfSpellcards = 8;


        int numberOfSpells = spellVariants.Length;
        //Debug.Log("numberofSpells =  " + numberOfSpells);

        int[] numberOfSpellsList = new int[numberOfSpells];
        numberOfSpellsList[0] = 1;
        int SpellCardCounter = 1;
        for (int i = 1; i < numberOfSpellsList.Length; i++)
        {
            
            numberOfSpellsList[i] = 2;
            SpellCardCounter += 2;
        }

        int counter = 1;
        while(SpellCardCounter< maxNumberOfSpellcards)
        {
            if (Random.Range(0, 2) == 1)
            {
                numberOfSpellsList[counter]++;
                SpellCardCounter++;

            }

            counter++;
            if (counter >= numberOfSpells)
                counter = 1;

        }

        SpellCardCounter = 0;
        for (int i = 0; i < spellVariants.Length; i++)
        {
            
            int numberOfRunes = Random.Range(1, 8); //[1,7]




            Debug.Log(spellVariants[i].ToString() + ": numberofSpellcards = " + numberOfSpellsList[i] + ". numberOfRunes = " + numberOfRunes);

            for (int j = 0; j < numberOfSpellsList[i]; j++)
            {
                m_SpellsOnPage.Add(Instantiate(spellVariants[i]));
                //on second run: fails to instantiate the first spell at the desired position, but the rest works fine. reason: Unknown
                //on third run: does not find m_SpellPositions[0] and throws an error. reason: ClearPage()
                m_SpellsOnPage[SpellCardCounter].transform.SetParent(m_SpellPositions[IndexOnPositionList].transform, false);

                SpellScript spellScript = m_SpellsOnPage[SpellCardCounter].transform.GetChild(0).GetComponent<SpellScript>();//SpellPrefab->Spell<SpellScript>

                if (!spellScript)
                    Debug.Log("no spellscript found");
                else
                    spellScript.DrawRunes(numberOfRunes);

                IndexOnPositionList++;
                maxNumberOfSpellcards -= numberOfSpellcards;
                SpellCardCounter = m_SpellsOnPage.Count;
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
            m_SpellsOnPage.Clear();
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
