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
    [SerializeField]
    private UnityEngine.UI.Slider m_healthSlider;
    [SerializeField]
    private UnityEngine.UI.Image m_hurtScreen;



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
        m_caveScript.EnemiesAhead += OpenBook;
        m_caveScript.StageComplete += CloseBook;
        m_hurtScreen.gameObject.SetActive(false);
    }

 
    public void TakeDamage(float damage, int element)
    {
        //AkSoundEngine.PostEvent("Combat_Player_damage", gameObject);
        m_fhealth -= damage;
        //Debug.Log(gameObject.name + ": Ouchie! I have " + m_fhealth + " healt left.");
        Debug.Log( m_fhealth + " health left.");
        m_hurtScreen.gameObject.SetActive(true);
        m_hurtScreen.CrossFadeAlpha(0.0f, .2f, false);
        StartCoroutine(ShowHurtScreen());
        m_healthSlider.value = m_fhealth/100.0f;
        if(m_fhealth<= 0)
        {
            //Debug.Log("Game Over.");
        }
    }

    IEnumerator ShowHurtScreen()
    {
        yield return new WaitForSeconds(.2f);
        m_hurtScreen.CrossFadeAlpha(.2f, .0f, false);
        m_hurtScreen.gameObject.SetActive(false);
    }

    private void OpenBook()
    {
        m_MagicBookScript.OpenBook();
    } 
    private void CloseBook()
    {
        m_MagicBookScript.CloseBook();
    }


}

