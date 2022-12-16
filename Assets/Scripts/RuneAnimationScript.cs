using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RuneAnimationScript : MonoBehaviour
{
    Animator anim;
    public string activeName;
    public string blackenName;
    public Action SpellSpent = delegate { };


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>() as Animator;
        if (!anim)
        {
            Debug.Log(transform.name + ": Animator not found");
        }
    }

    public void PlayActiveAnimation()
    {
        anim.Play(activeName);
    } 
    public void PlayBlackenAnimation()
    {
        //Debug.Log(this.name + blackenName);
        anim.Play(blackenName);
    }
    public void LetSpellBeDeleted()
    {
        SpellSpent();
    }

  
}
