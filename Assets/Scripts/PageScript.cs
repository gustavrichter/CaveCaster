using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PageScript : MonoBehaviour
{

    
    [SerializeField]
    private GameObject[] m_SpellPositions; //size=18

    private GameObject[] m_SpellsOnPage;
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

        //shuffle transform list
        ShuffleList(m_SpellPositions);
        //shuffle spellvariants
        ShuffleList(spellVariants);

        //add most unique
        m_SpellsOnPage[0] = Instantiate(spellVariants[0], m_SpellPositions[0].transform); 

        SpellScript spell = m_SpellsOnPage[0].GetComponent<SpellScript>();

        spell.DrawRunes(Random.Range(1, 3));

        for (int i = 1; i < spellVariants.Length; i++)
        {
            int indexPos = 1;
            int numberOfSpellcards = Random.Range(2, 5);
            int numberOfRunes = Random.Range(2, 3);
            int riter = m_SpellsOnPage.GetUpperBound(0);
            for (int j = 0; j < numberOfSpellcards; j++)
            {
                m_SpellsOnPage[riter+j] = Instantiate(spellVariants[i], m_SpellPositions[indexPos].transform);
                spell = m_SpellsOnPage[riter + j].GetComponent<SpellScript>();
                spell.DrawRunes(numberOfRunes);
            }
                
        }

        
        
    }

    private void ClearPage()
    {
        for (int i = 0; i < m_SpellsOnPage.Length; i++)
        {
            Destroy(m_SpellsOnPage[i]);
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
