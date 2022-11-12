using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    int m_ihealth;
    public GameObject m_MagicBook;
    [SerializeField]
    private MagicBookScript m_MagicBookScript;

    void Start()
    {
        m_MagicBookScript = m_MagicBook.GetComponent<MagicBookScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            m_MagicBookScript.OpenBook();
        }
    }
}
