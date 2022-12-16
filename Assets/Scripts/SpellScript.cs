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
    public Action SpellFired = delegate { };

   
    [SerializeField] private GameObject[] m_SpellPositions;//size=7

    private List<GameObject> m_RunesOnSpell = new List<GameObject>();// dynamic size

    //set positions for all number variations
    private int[] m_positions_0 = new int[] { 0, 0, 0, 0, 0, 0, 0};
    private int[] m_positions_1 = new int[] { 1, 0, 0, 0, 0, 0, 0};
    private int[] m_positions_2 = new int[] { 0, 0, 1, 0, 0, 1, 0};
    private int[] m_positions_3 = new int[] { 1, 0, 0, 1, 0, 0, 1};
    private int[] m_positions_4 = new int[] { 0, 1, 0, 1, 1, 0, 1};
    private int[] m_positions_5 = new int[] { 1, 1, 1, 0, 1, 1, 0};
    private int[] m_positions_6 = new int[] { 0, 1, 1, 1, 1, 1, 1};
    private int[] m_positions_7 = new int[] { 1, 1, 1, 1, 1, 1, 1};

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

        }
       
    }
    private void Start()
    {
        m_dragScript = GetComponentInParent<DragOnEnemy>() as DragOnEnemy;
        if (!m_dragScript)
            Debug.Log("dragsrpit not found");
        m_dragScript.SpellCardDropped += FiringSpell;

    }
    private void OnDestroy()
    {
        m_dragScript.SpellCardDropped -= FiringSpell;
    }
    private void FiringSpell()
    {
        //do raycast
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (rayHit)//ray hit something
        {
            Debug.Log("Hit: " + rayHit.transform.gameObject.name);
            if (rayHit.transform.gameObject.tag == "Enemy") //if it hit enemy
            {
                EnemyScript enemyScript = rayHit.transform.gameObject.GetComponent<EnemyScript>();
                if (!enemyScript)
                    Debug.Log("enemyscr not found");
                else if (bunique)//only do damage if spell is unique
                {
                    enemyScript.TakeDamage(m_baseDamage, m_element);
                }
                SpellFired(); //always fire spell when enemy is hit to make the page turn
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
                if (m_element == 0)
                {
                    //string name = transform.name;
                    //Debug.Log("This Object: " + name + ".ChildCount = " + transform.childCount);
                    // name = m_RunesOnSpell[i].gameObject.name;
                    //Debug.Log(name + ".ChildCount = " +m_RunesOnSpell[i].transform.childCount);
                    m_runeAnimators.Add(m_RunesOnSpell[i].gameObject.GetComponent<RuneAnimationScript>());
                    m_runeAnimators[m_runeAnimators.Count - 1].PlayIdleAnimation();
                }
               
            }
        }
        //Debug.Log("Active Self: " +
        //    "[" + m_RunesOnSpell[0].activeSelf + 
        //    ", " + m_RunesOnSpell[1].activeSelf + 
        //    ", " + m_RunesOnSpell[2].activeSelf + 
        //    ", " + m_RunesOnSpell[3].activeSelf + 
        //    ", " + m_RunesOnSpell[4].activeSelf + 
        //    ", " + m_RunesOnSpell[5].activeSelf + 
        //    ", " + m_RunesOnSpell[6].activeSelf + "]");
        //Debug.Log("Active in Hierarchy: " +
        //    "[" + m_RunesOnSpell[0].activeInHierarchy +
        //    ", " + m_RunesOnSpell[1].activeInHierarchy +
        //    ", " + m_RunesOnSpell[2].activeInHierarchy +
        //    ", " + m_RunesOnSpell[3].activeInHierarchy +
        //    ", " + m_RunesOnSpell[4].activeInHierarchy +
        //    ", " + m_RunesOnSpell[5].activeInHierarchy +
        //    ", " + m_RunesOnSpell[6].activeInHierarchy + "]");

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
}
