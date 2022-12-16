using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SpellcardVariant
{
    public int element;
    public int numberOfRunes;
    public int numberOfSpellcards;
    public SpellcardVariant(int el, int numRunes, int numSpellcards)
    {
        element = el;
        numberOfRunes = numRunes;
        numberOfSpellcards = numSpellcards;
    }

}

public class PageScript : MonoBehaviour
{
    
    [SerializeField] private GameObject[] m_SpellPositions; //size=10

    private List<GameObject> m_SpellsOnPage; //dynamic size
    private List<SpellScript> m_SpellScripts; //dynamic size
    private List<SpellcardVariant> m_SpellcardVariants;
    private void Awake()
    {
        m_SpellsOnPage = new List<GameObject>();
        m_SpellScripts = new List<SpellScript>();
        m_SpellcardVariants = new List<SpellcardVariant>();

    }
    public void NextPage(GameObject[] spellVariants)
    {
        //shuffle transform list to place the spellcards randomly
        ShuffleList(m_SpellPositions);
        //create the different types of spellcards
        CreateSpellcardVariants(spellVariants);

        int SpellCardCounter = 0;
        for (int i = 0; i < spellVariants.Length; i++)
        {
            //Debug.Log(spellVariants[i].ToString() + ": numberofSpellcards = " + numberOfSpellsList[i] + ". numberOfRunes = " + numberOfRunes);

            for (int j = 0; j < m_SpellcardVariants[i].numberOfSpellcards; j++)
            {
                m_SpellsOnPage.Add(Instantiate(spellVariants[i]));
                m_SpellsOnPage[SpellCardCounter].transform.SetParent(m_SpellPositions[SpellCardCounter].transform, false);
                m_SpellScripts.Add(m_SpellsOnPage[SpellCardCounter].transform.GetChild(0).GetComponent<SpellScript>());//SpellPrefab->Spell<SpellScript>

                if (m_SpellScripts[SpellCardCounter])
                {
                    if (i == 0)
                        m_SpellScripts[SpellCardCounter].bunique = true;

                    m_SpellScripts[SpellCardCounter].DrawRunes(m_SpellcardVariants[i].numberOfRunes);
                    SpellCardCounter++;
                }
                else
                {
                    Debug.Log("no spellscript found");
                }

            }
        }
    }
    void RevealUnique()
    {
    }
    void CreateSpellcardVariants(GameObject[] spellVariants) {

        m_SpellcardVariants.Clear();
        SpellcardVariant temp = new SpellcardVariant();

        //element
        for (int i = 0; i < spellVariants.Length; i++)
        {
            m_SpellcardVariants.Add(new SpellcardVariant(0, 0, 0));
            SpellScript sp = spellVariants[i].transform.GetChild(0).GetComponent<SpellScript>();
            if (!sp)
                Debug.Log("spellscript not found");
            temp = m_SpellcardVariants[i];
            temp.element = sp.getElement();
            m_SpellcardVariants[i] = temp;
        }

        //numberOfSpellcards
        int maxNumberOfSpellcards = 10;
        temp = m_SpellcardVariants[0];
        temp.numberOfSpellcards = 1;//set amount of spellcards for first spellcarcvariant in the list, which will be the most unique
        m_SpellcardVariants[0] = temp;
        int spellcardCount = 1;

        for (int i = 1; i < m_SpellcardVariants.Count; i++)
        {
            //the rest of the spellcardvariants have always at least 2 spellcards
            temp = m_SpellcardVariants[i];
            temp.numberOfSpellcards = 2;
            spellcardCount += 2;
            m_SpellcardVariants[i] = temp;

        }

        int index = 1; //count from 1 because we dont want to change the amount of spellcard for the unique spellcardvariant which sits at index = 0
        do
        {
            //only add a spellcard with a 50/50 chance ->  better variation
            if (Random.Range(0, 2) == 1)
            {
                temp = m_SpellcardVariants[index];
                temp.numberOfSpellcards++;
                m_SpellcardVariants[index] = temp;
                spellcardCount++;
            }
            index++;
            if (index >= m_SpellcardVariants.Count)
                index = 1;
        } while (spellcardCount < maxNumberOfSpellcards); //until all spellcard positions can get a spellcard

        //numberOfRunes
        int numberOfRuneVariants = 3; //always have 3 different rune patterns
        int[] randRunes = { 0, 0, 0 };

        for (int i = 0; i < numberOfRuneVariants; i++)
        {
            bool numberTaken;
            //this do while loop ensures that there are 3 different numbers in the randRunes array
            do
            {
                randRunes[i] = Random.Range(1, 8);
                numberTaken = false;


                for (int j = 0; j < numberOfRuneVariants; j++)
                {
                    if (i != j && randRunes[i] == randRunes[j])
                        numberTaken = true;
                }

            } while (numberTaken);
        }

        //initialise all spellcardvariants with 0 runes
        for (int i = 0; i < m_SpellcardVariants.Count; i++)
        {
            temp = m_SpellcardVariants[i];
            temp.numberOfRunes = 0;
            m_SpellcardVariants[i] = temp;
        }
     
        //decide rune pattern for all variants
        for (int i = 0; i < m_SpellcardVariants.Count; i++)
        {
            if (i == 0)//if unique
            {
                temp = m_SpellcardVariants[0];
                temp.numberOfRunes = randRunes[0];//the unique spell (which sits in m_SpellcardVariants[0] gets the rune pattern in randRunes[0]
                m_SpellcardVariants[0] = temp;
            }
            else
            {
                int lowerBound = 0;
                if (m_SpellcardVariants[i].element == m_SpellcardVariants[0].element)//if it is the same element as unique it cannot have the rune pattern in randRunes[0]
                {
                    lowerBound = 1;
                }

                temp = m_SpellcardVariants[i];
                temp.numberOfRunes = randRunes[Random.Range(lowerBound, numberOfRuneVariants)];
                m_SpellcardVariants[i] = temp;
            }
        }
        for (int i = 0; i < m_SpellcardVariants.Count; i++)
        {
            Debug.Log("SpellcardVariant_" + i + ",: #cards = " + m_SpellcardVariants[i].numberOfSpellcards + ". element = " + m_SpellcardVariants[i].element + ". #runes = " + m_SpellcardVariants[i].numberOfRunes);
        }
    }

    public void ClearPage()
    {
        if (m_SpellsOnPage.Count > 0)
        {
            for (int i = 0; i < m_SpellsOnPage.Count; i++)
            {
                //Debug.Log("destroying SpellsOnPage[" + i + "]");
                Destroy(m_SpellsOnPage[i]);
                
            }
            m_SpellsOnPage.Clear();
            m_SpellScripts.Clear();
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

    public List<SpellScript> GetSpellScripts()
    {
        if (m_SpellScripts.Count > 0)
        {
            return m_SpellScripts;
        }
        return null;
    }
}
