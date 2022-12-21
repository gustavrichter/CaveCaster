using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField] private int m_element;
    [SerializeField] private DragOnEnemy m_dragScript;
    private List<RuneAnimationScript> m_runeAnimators = new List<RuneAnimationScript>(); //only animators of active Runes 
    private int m_numberOfRunes;
    private int m_baseDamage = 50;
    public GameObject m_Rune;
    public bool wasFired = false;
    public bool bunique = false;
    public int m_index;
    public int m_variant;
    public Action<int> SpellFired = delegate { };
    public Action SpellSpent = delegate { };

   
    [SerializeField] private GameObject[] m_SpellPositions;//size=7

    private List<GameObject> m_RunesOnSpell = new List<GameObject>();// dynamic size

    //set positions for all number variations
    //new positions:    even -> /      odd -> \
    private int[] m_positions_0 = new int[] { 0, 0, 0, 0, 0, 0, 0};
    private int[] m_positions_1 = new int[] { 1, 0, 0, 0, 0, 0, 0};
    private int[] m_positions_2 = new int[] { 0, 1, 1, 0, 0, 0, 0};
    private int[] m_positions_3 = new int[] { 1, 0, 0, 1, 1, 0, 0};
    private int[] m_positions_4 = new int[] { 0, 1, 1, 0, 0, 1, 1};
    private int[] m_positions_5 = new int[] { 1, 1, 1, 1, 1, 0, 0};
    private int[] m_positions_6 = new int[] { 0, 1, 1, 1, 1, 1, 1};
    private int[] m_positions_7 = new int[] { 1, 1, 1, 1, 1, 1, 1};
    /*old:
    private int[] m_positions_0 = new int[] { 0, 0, 0, 0, 0, 0, 0};
    private int[] m_positions_1 = new int[] { 1, 0, 0, 0, 0, 0, 0};
    private int[] m_positions_2 = new int[] { 0, 0, 1, 0, 0, 1, 0};
    private int[] m_positions_3 = new int[] { 1, 0, 0, 1, 0, 0, 1};
    private int[] m_positions_4 = new int[] { 0, 1, 0, 1, 1, 0, 1};
    private int[] m_positions_5 = new int[] { 1, 1, 1, 0, 1, 1, 0};
    private int[] m_positions_6 = new int[] { 0, 1, 1, 1, 1, 1, 1};
    private int[] m_positions_7 = new int[] { 1, 1, 1, 1, 1, 1, 1};
    */

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
            m_RunesOnSpell.Add(Instantiate(m_Rune, m_SpellPositions[i].transform) as GameObject);
            m_RunesOnSpell[i].SetActive(false);
            //m_runeAnimators.Add(m_RunesOnSpell[i].gameObject.GetComponent<RuneAnimationScript>());

        }
       
    }
    private void Start()
    {
        m_dragScript = GetComponentInParent<DragOnEnemy>() as DragOnEnemy;
        if (!m_dragScript)
        {
            //Debug.Log("dragsrpit not found");
        }
        else
            m_dragScript.SpellCardDropped += FiringSpell;

    }
    private void OnDestroy()
    {
        if(m_dragScript)
            m_dragScript.SpellCardDropped -= FiringSpell;
    }
    private void FiringSpell()
    {
        //do raycast
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (rayHit)//ray hit something
        {
            //Debug.Log("Hit: " + rayHit.transform.gameObject.name);
            if (rayHit.transform.gameObject.tag == "Enemy") //if it hit enemy
            {
                EnemyScript enemyScript = rayHit.transform.gameObject.GetComponent<EnemyScript>();
                if (!enemyScript)
                    Debug.Log("enemyscr not found");
                else if (bunique)//only do damage if spell is unique
                {
                    enemyScript.TakeDamage(m_baseDamage, m_element);
                }
                SpellFired(m_index); //always fire spell when enemy is hit to make the page turn
            }
        }
        else
        {
            Debug.Log("No target hit");
        }

        m_dragScript.ResetPosition();
    }
    public void DrawRunes(int amount)
    {
        m_numberOfRunes = amount;
        int[] pos = GetPositionList();
        //Debug.Log("Rune Pattern: element=" + m_element);           
        if (pos == null)
        {
            Debug.Log("no PositionList returned. No Runes will be drawn.");
            pos = m_positions_0;
        }

        for (int i = 0; i < m_RunesOnSpell.Count; i++)
        {
            bool bsetactive = pos[i] == 1;

            if (bsetactive)
            {
                m_RunesOnSpell[i].SetActive(true);
                m_runeAnimators.Add(m_RunesOnSpell[i].gameObject.GetComponent<RuneAnimationScript>());
            }
        }
    
    }
    public void BindSpellSpentAction()
    {
      
        //Debug.Log("animation count " + m_runeAnimators.Count);
        m_runeAnimators[0].SpellSpent += SpentSpell;

    }
    void SpentSpell()
    {
        //Debug.Log("SpendingSpell");
        SpellSpent();
        m_runeAnimators[0].SpellSpent -= SpentSpell;
    }

    int[] GetPositionList()
    {
        //Debug.Log("m_aPositions.Lenth = " + m_aPositions.Length);
        for (int i = 0; i < m_aPositions.Length; i++)
        {
            if (m_aPositions[i].numberOfRunes == m_numberOfRunes)
            {
                return m_aPositions[i].m_activePositions;
            }
        }
        //failed: amount not found
        return null;

    }

    public int getDamage()
    {

        return m_baseDamage;// *numberOfRunes;
    }
    public int getElement()
    {
        return m_element;
    }
    public int getNumberOfRunes()
    {
        return m_numberOfRunes;
    }
    public void setNumberOfRunes(int numRunes)
    {
        m_numberOfRunes = numRunes;
    }

    public void FadeOut()
    {
        //Debug.Log("animators.Count = " + m_runeAnimators.Count);
        for (int i = 0; i < m_runeAnimators.Count; i++)
        {
            m_runeAnimators[i].PlayBlackenAnimation();
            //m_runeAnimators[i].PlayActiveAnimation();
        }

    }
}
