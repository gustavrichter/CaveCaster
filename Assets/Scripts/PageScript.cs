using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PageScript : MonoBehaviour
{

    
    [SerializeField]
    private GameObject[] m_SpellPositions; //size=18

    private SpellScript[] m_SpellsOnPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {

    }

    private void ClearPage()
    {
        //delete m_SpellsOnPage
    }
}
