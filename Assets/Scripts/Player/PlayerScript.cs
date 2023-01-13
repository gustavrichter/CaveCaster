using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK;
using System;
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
    [SerializeField]
    private TextMeshProUGUI inkAmountText;

    [SerializeField]
    private UnityEngine.UI.Button m_pauseButton;
    [SerializeField]
    private UnityEngine.UI.Button m_exitButton;
    [SerializeField]
    private UnityEngine.UI.Button m_resumeButton;
    [SerializeField]
    private UnityEngine.UI.Button m_restartButton;

    //[SerializeField]
    //AK.Wwise.State fightState;
    //[SerializeField]
    //AK.Wwise.Event event_menu;


    public Action PlayerDeath = delegate { };
    public Action RestartTheGame = delegate { };


    //int m_amountHealthPotions;
    //int m_amountInkBottles;


    private void Awake()
    {
        //fightState.SetValue();
        //m_amountHealthPotions = 3;
        //m_amountInkBottles = 3;
        //healthAmountText.text = m_amountHealthPotions.ToString();
        //inkAmountText.text = m_amountInkBottles.ToString();
        isAlive = true;
        m_fhealth = 100;
        //m_MagicBook = Instantiate(m_MagicBook.gameObject);
        //m_Cave = Instantiate(m_Cave.gameObject);
        m_MagicBook = GameObject.FindGameObjectWithTag("MagicBook");
        m_Cave = GameObject.FindGameObjectWithTag("Cave");
       
    }
   
    void Start()
    {

        m_MagicBookScript = m_MagicBook.GetComponent<MagicBookScript>();
        m_caveScript = m_Cave.GetComponent<CaveScript>();
        m_caveScript.EnemiesAhead += OpenBook;
        m_caveScript.EnemiesAhead += PlayFightMusic;
        //m_caveScript.EnemiesAhead += SetFightState;
        m_caveScript.StageComplete += CloseBook;
        m_caveScript.StageComplete += PlayExploreMusic;
        m_healthSlider.value = m_fhealth / 100.0f;

        m_hurtScreen.gameObject.SetActive(false);
        m_exitButton.gameObject.SetActive(false);
        m_pauseButton.gameObject.SetActive(true);
        m_restartButton.gameObject.SetActive(false);
        m_resumeButton.gameObject.SetActive(false);
        //fightState.SetValue();
        //event_menu.Post(gameObject);
        PlayExploreMusic();

    }

    public void PauseGame()
    {
        AkSoundEngine.PostEvent("Menu_back", gameObject);
        ////these are not found
        //Debug.Log("Posting State_Death. ID:2837885159");
        //AkSoundEngine.PostEvent("State_Death", gameObject);//2837885159

        //Debug.Log("Posting State_Menu. ID:3900992838");
        //AkSoundEngine.PostEvent("State_Menu", gameObject);//3900992838
        //Debug.Log("Posting Music_Menu. ID:1598298728");
        //AkSoundEngine.PostEvent("Music_Menu", gameObject);//1598298728

        //Debug.Log("Posting State_Fight. ID:1496372043");
        //AkSoundEngine.PostEvent("State_Fight", gameObject);//1496372043

        //Debug.Log("Posting State_Explore. ID:571235964");
        //AkSoundEngine.PostEvent("State_Explore", gameObject);//571235964
        //PlayMenuExploreMusic();
        m_exitButton.gameObject.SetActive(true);
        m_restartButton.gameObject.SetActive(true);
        m_resumeButton.gameObject.SetActive(true);
        m_pauseButton.gameObject.SetActive(false);
        m_MagicBookScript.ShowPausePage();
        m_caveScript.PauseEnemies();

    }
    //public void SetFightState()
    //{
    //    fightState.SetValue();
    //}

    public void ResumeGame()
    {
        AkSoundEngine.PostEvent("Menu_accept", gameObject);
        PlayExploreMusic();
        m_exitButton.gameObject.SetActive(false);
        m_pauseButton.gameObject.SetActive(true);
        m_restartButton.gameObject.SetActive(false);
        m_resumeButton.gameObject.SetActive(false);
        m_MagicBookScript.HidePausePage();
        m_caveScript.ResumeEnemies();
    }

    public void RestartGame()
    {
        AkSoundEngine.PostEvent("Menu_accept", gameObject);
        PlayExploreMusic();
        m_exitButton.gameObject.SetActive(false);
        m_pauseButton.gameObject.SetActive(true);
        m_restartButton.gameObject.SetActive(false);
        m_resumeButton.gameObject.SetActive(false);
        m_caveScript.StopStage();
        m_MagicBookScript.HidePausePage();
        //m_caveScript.StartCoroutine("DelayedSpawnEnemies", 1.0f);
        //Debug.Log(gameObject.name + "restarting game");
        HealPlayer(200.0f);
        RestartTheGame();
    }


    public void ExitToMenu()
    {
        AkSoundEngine.PostEvent("Menu_back", gameObject);
        PlayMenuExploreMusic();
        //switch scene to Menu scene
        //or do menu overlay
    }

    public void TakeDamage(float damage, int element)
    {
        AkSoundEngine.PostEvent("Player_damage", gameObject);
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
            PlayDeathMusic();
            PlayerDeath();
            //Debug.Log("Game Over.");
        }
    }

    //public void UseMagicInk()
    //{
    //    if (m_amountInkBottles > 0)
    //    {
    //        m_MagicBookScript.InkReveal();
    //        m_amountInkBottles--;
    //        inkAmountText.text = m_amountInkBottles.ToString();

    //    }
    //}

    //public void AddHealthPotion()
    //{
    //    m_amountHealthPotions++;
    //    healthAmountText.text = m_amountHealthPotions.ToString();
    //}

    //public void AddInkBottle()
    //{
    //    m_amountInkBottles++;
    //    inkAmountText.text = m_amountInkBottles.ToString();
    //}
    public void HealPlayer(float healAmount)
    {
        //if (m_amountHealthPotions > 0)
        //{
            m_fhealth += healAmount;
            if (m_fhealth > 100.0f)
            {
                m_fhealth = 100.0f;
            }
            m_healthSlider.value = m_fhealth / 100.0f;
            //m_amountHealthPotions--;
            //healthAmountText.text = m_amountHealthPotions.ToString();
        //}
    }
    IEnumerator ShowHurtScreen()
    {
        yield return new WaitForSeconds(.2f);
        //Debug.Log("Showing HurtScreen");
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
        //if (isAlive)
        //{
        //    m_caveScript.StartCoroutine("DelayedSpawnEnemies", 3.0f);
        //}
    }
    private void PlayFightMusic()
    {
        AkSoundEngine.PostEvent("Music_Fight", gameObject);
    }

    private void PlayExploreMusic()
    {
        AkSoundEngine.PostEvent("Music_Explore", gameObject);
    }

    private void PlayMenuExploreMusic()
    {
        AkSoundEngine.PostEvent("Music_Menu", gameObject);
    }

    //private void PlayMenuExploreMusic()
    //{
    //    AkSoundEngine.PostEvent("Menu_Effect", gameObject);
    //}

    public void PlayDeathMusic()
    {
        AkSoundEngine.PostEvent("Music_Death", gameObject);
    }
}

