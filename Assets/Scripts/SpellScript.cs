using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Positions
{
    public int numberOfSpells;
    public int[] m_activePositions;
   
    
    public Positions(int num, int[] pos)
    {
        numberOfSpells = num;
        m_activePositions = pos;
    }

}
public  class SpellScript : MonoBehaviour
{
    private int m_damage;
    public GameObject m_Rune;
    [SerializeField]
    private GameObject[] m_SpellPositions;//size=9

    private GameObject[] m_RunesOnSpell;

    //set positions for all number variations
    private int[] m_positions_1 = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
    private int[] m_positions_2 = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 };
    private int[] m_positions_3 = new int[] { 0, 0, 1, 0, 1, 0, 1, 0, 0 };

    //private Positions[] m_aPositions = new Positions[] { new Positions(), new Positions(), new Positions()};
    private Positions[] m_aPositions = null;

   


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SpellScript Start()");
        
        
        
        
        Debug.Log(m_positions_1[4]);

        m_aPositions[0] = new Positions(1, m_positions_1);

        m_aPositions[0].m_activePositions = m_positions_1;
        m_aPositions[0].numberOfSpells = 1;

        //am anfang alle spells draufzeichnnen und erstmal deaktivieren
        for (int i = 0; i < m_SpellPositions.Length; i++)
        {
            m_RunesOnSpell[i] = Instantiate(m_Rune, m_SpellPositions[i].transform);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawRunes(int amount)
    {

        int[] pos = GetPositionList(amount);


        for (int i = 0; i < m_RunesOnSpell.Length; i++)
        {
            if (pos[i] == 1)
            {
                m_RunesOnSpell[i].SetActive(true);
            }
        }

    }
    void OnMouseDown()
    {
        Debug.Log("Spell clicked.");
    }

    int[] GetPositionList(int amount)
    {
        for (int i = 0; i < m_aPositions.Length; i++)
        {
            if (m_aPositions[i].numberOfSpells == amount)
            {
                return m_aPositions[i].m_activePositions;
            }
        }
        //failed: amount not found
        return null;

    }
}
