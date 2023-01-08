using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float m_fhealth;
    public bool isAlive;
    public GameObject m_MagicBook;
    private MagicBookScript m_MagicBookScript;
    public GameObject m_Cave;
    private CaveScript m_caveScript;
    [SerializeField]
    private UnityEngine.UI.Slider m_healthSlider;
    [SerializeField]
    private UnityEngine.UI.Image m_hurtScreen;
    [SerializeField]
    private TextMeshProUGUI healthAmountText;
    //private UnityEngine.UI.Text healthAmountText;
    [SerializeField]
    private TextMeshProUGUI inkAmountText;


    int m_amountHealthPotions;
    int m_amountInkBottles;


    private void Awake()
    {
        m_amountHealthPotions = 3;
        m_amountInkBottles = 3;
        healthAmountText.text = m_amountHealthPotions.ToString();
        inkAmountText.text = m_amountInkBottles.ToString();
        isAlive = true;
        m_fhealth = 100;
        //m_MagicBook = Instantiate(m_MagicBook.gameObject);
        //m_Cave = Instantiate(m_Cave.gameObject);
        m_MagicBook = GameObject.Find("MagicBook");
<<<<<<< HEAD
        m_Cave = GameObject.Find("CaveMaxi");
        //m_Cave = GameObject.Find("Cave");
=======
        m_Cave = GameObject.FindGameObjectWithTag("Cave");
        
>>>>>>> main
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
            isAlive = false;
            //Debug.Log("Game Over.");
        }
    }

    public void UseMagicInk()
    {
        if (m_amountInkBottles > 0)
        {
            m_MagicBookScript.InkReveal();
            m_amountInkBottles--;
            inkAmountText.text = m_amountInkBottles.ToString();

        }
    }

    public void AddHealthPotion()
    {
        m_amountHealthPotions++;
        healthAmountText.text = m_amountHealthPotions.ToString();
    }

    public void AddInkBottle()
    {
        m_amountInkBottles++;
        inkAmountText.text = m_amountInkBottles.ToString();
    }
    public void HealPlayer(float healAmount)
    {
        if (m_amountHealthPotions > 0)
        {
            m_fhealth += healAmount;
            if (m_fhealth > 100.0f)
            {
                m_fhealth = 100.0f;
            }
            m_healthSlider.value = m_fhealth / 100.0f;
            m_amountHealthPotions--;
            healthAmountText.text = m_amountHealthPotions.ToString();
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

