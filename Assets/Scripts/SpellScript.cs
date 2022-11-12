using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SpellScript : MonoBehaviour
{
    private int m_damage;
    [SerializeField]
    private GameObject[] m_SpellPositions;//size=9
    public GameObject m_Rune;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawRunes(int[] positions)
    {

    }
    private void OnMouseDown()
    {
        Debug.Log("Spell clicked.");
    }
}
