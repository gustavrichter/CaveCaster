using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float m_fhealth = 100.0f;
    public GameObject m_MagicBook;
    [SerializeField]
    private MagicBookScript m_MagicBookScript;
    public GameObject m_Cave;
    private CaveScript m_caveScript;

    private void OnEnable()
    {
        m_fhealth = 100.0f;
    }
    void Start()
    {
        //Debug.Log("my health: " + m_fhealth);

        m_MagicBookScript = m_MagicBook.GetComponent<MagicBookScript>();
        m_caveScript = m_Cave.GetComponent<CaveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("my health: " + m_fhealth);

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
        Debug.Log("my health: " + m_fhealth);
        m_fhealth -= damage;
        Debug.Log(gameObject.name + ": Ouchie! I have " + m_fhealth + " healt left.");

        if(m_fhealth<= 0)
        {
            Debug.Log("Game Over.");
        }
    }

}

