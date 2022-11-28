using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float m_fhealth;
    public GameObject m_MagicBook;
    private MagicBookScript m_MagicBookScript;
    public GameObject m_Cave;
    private CaveScript m_caveScript;
    // private bool healthWasSet = false;
    // private bool healthWasSet2 = false;

    private void Awake()
    {
        m_fhealth = 100;
        //m_MagicBook = Instantiate(m_MagicBook.gameObject);
        //m_Cave = Instantiate(m_Cave.gameObject);
        m_MagicBook = GameObject.Find("MagicBook");
        m_Cave = GameObject.Find("Cave");
    }
   
    void Start()
    {

        m_MagicBookScript = m_MagicBook.GetComponent<MagicBookScript>();
        m_caveScript = m_Cave.GetComponent<CaveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.O))
        {
            m_MagicBookScript.OpenBook();
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            m_MagicBookScript.TurnPage();

        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_caveScript.SpawnEnemy();

        }
    }
    public void TakeDamage(float damage, int element)
    {
        
        m_fhealth -= damage;
        Debug.Log(gameObject.name + ": Ouchie! I have " + m_fhealth + " healt left.");

        if(m_fhealth<= 0)
        {
            Debug.Log("Game Over.");
        }
    }

}

