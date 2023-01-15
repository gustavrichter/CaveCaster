//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SceneSwitcher : MonoBehaviour
//{
//    public Animator transiton;

//    public float transitionTime = 1f;

//    public void playGame()
//    {
//        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
//    }
////    public void Quit()
////    {
////#if UNITY_STANDALONE
////        Application.Quit();
////#endif
////#if UNITY_EDITOR
////        UnityEditor.EditorApplication.isPlaying = false;
////#endif
////    }
//    IEnumerator LoadLevel(int levelIndex)
//    {
//        transiton.SetTrigger("Start");

//        yield return new WaitForSeconds(transitionTime);

//        SceneManager.LoadScene(levelIndex);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Animator levelTransition;
    public float transitionTime = 1f;
    public PlayerScript playerScript;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            AkSoundEngine.PostEvent("Music_Menu", gameObject);
        }
        else
        {
            playerScript.PlayExploreMusic();
        }
    }
    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AkSoundEngine.StopAll();
        }
        else
        {
            playerScript.StopAllAudio();
        }

    }
    public void playGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void returnToMenu()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void Back()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public IEnumerator LoadLevel(int levelIndex)
    {
        levelTransition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelIndex);
    }
}