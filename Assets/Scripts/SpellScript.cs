using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Positions
{
    public int numberOfRunes;
    public int[] m_activePositions;
   
    
    public Positions(int num, int[] pos)
    {
        numberOfRunes = num;
        m_activePositions = pos;
    }

}
public  class SpellScript : MonoBehaviour
{
    private int m_damage;
    public GameObject m_Rune;
    [SerializeField] private GameObject[] m_SpellPositions;//size=9

    private List<GameObject> m_RunesOnSpell = new List<GameObject>();// dynamic size

    //set positions for all number variations
    private int[] m_positions_1 = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
    private int[] m_positions_2 = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 };
    private int[] m_positions_3 = new int[] { 0, 0, 1, 0, 1, 0, 1, 0, 0 };
    private int[] m_positions_4 = new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 1 };
    private int[] m_positions_5 = new int[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 };
    private int[] m_positions_6 = new int[] { 1, 0, 1, 1, 0, 1, 1, 0, 1 };
    private int[] m_positions_7 = new int[] { 1, 0, 1, 1, 1, 1, 1, 0, 1 };

    //private Positions[] m_aPositions = new Positions[] { new Positions(), new Positions(), new Positions()};
    private Positions[] m_aPositions = new Positions[7];

   


    // Start is called before the first frame update
    void Awake()
    {
        m_aPositions[0] = new Positions(1, m_positions_1);
        m_aPositions[1] = new Positions(2, m_positions_2);
        m_aPositions[2] = new Positions(3, m_positions_3);
        m_aPositions[3] = new Positions(4, m_positions_4);
        m_aPositions[4] = new Positions(5, m_positions_5);
        m_aPositions[5] = new Positions(6, m_positions_6);
        m_aPositions[6] = new Positions(7, m_positions_7);


        //am anfang alle spells draufzeichnnen und erstmal deaktivieren
        for (int i = 0; i < m_SpellPositions.Length; i++)
        {
            m_RunesOnSpell.Add(Instantiate(m_Rune, m_SpellPositions[i].transform));
            m_RunesOnSpell[i].SetActive(false);
            //m_RunesOnSpell[i].transform.SetParent(m_SpellPositions[i].transform, false);


        }
    }

    public void MouseClicked()
    {

        Debug.Log("Spell clicked. (MouseClicked)");

    }
    void OnMouseDown()
    {
        Debug.Log("Spell clicked. (OnMouseDown)");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Spell clicked (update)");
        }
    }

    public void DrawRunes(int amount)
    {

        int[] pos = GetPositionList(amount);

        if (pos == null)
            Debug.Log("no PositionList returned");

        for (int i = 0; i < m_RunesOnSpell.Count; i++)
        {

            m_RunesOnSpell[i].SetActive(pos[i]==1);
        }
    }
  

    int[] GetPositionList(int amount)
    {
        //Debug.Log("m_aPositions.Lenth = " + m_aPositions.Length);
        for (int i = 0; i < m_aPositions.Length; i++)
        {
            if (m_aPositions[i].numberOfRunes == amount)
            {
                return m_aPositions[i].m_activePositions;
            }
        }
        //failed: amount not found
        return null;

    }
}
