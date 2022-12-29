using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RuneAnimationScript : MonoBehaviour
{
    Animator anim;
    public string activeName;
    public string rapidflashName;
    public Action SpellSpent = delegate { };
    public int iLoop = 0;
    private bool isunique;

    // Start is called before the first frame update
    void Awake()
    {
        isunique = false;
        anim = GetComponent<Animator>() as Animator;
        if (!anim)
        {
            Debug.Log(transform.name + ": Animator not found");
        }
    }
    public void setUnique(bool uniq)
    {
        isunique = uniq;
        Debug.Log("setting Uniqe: " + uniq + ", " + isunique);
    }
    public void PlayActiveAnimation()
    {
        Debug.Log("Playing active animation");
        anim.Play(activeName);
    } 
    public void PlayBlackenAnimation()
    {
        //Debug.Log(this.name + blackenName);
        anim.Play(rapidflashName);
    }
    public void LetSpellBeDeleted()
    {
        //if (!isunique)
        //{

        //    iLoop = 4;
        //}
        //Debug.Log("is unique: " + isunique);
        //Debug.Log("Looping: " + iLoop);
        if (iLoop == 1)
        {
            SpellSpent();
        }
        else
        {
            iLoop++;
        }
        
    }

  
}
