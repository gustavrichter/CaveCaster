using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour
{
    Animator anim;
    public string attackName;
    public string damagedName;
    public string damagedCritName;
    public string defeatName;
    EnemyScript myenemyScript;
    void Awake()
    {
        anim = GetComponent<Animator>();
        myenemyScript = GetComponent<EnemyScript>();
        if (!anim)
        {
            Debug.Log(transform.name + ": Animator not found");
        }
    }

    public void PlayAttackAnimation()
    {
        //Debug.Log(transform.name + ": Playing Attack Animation");
        if (!myenemyScript.animating)
        {
            //interruptible
            anim.Play(attackName);
        }
    }

    public void PlayDamagedAnimation()
    {
        //Debug.Log(transform.name + ": Playing Damaged Animation");
        
        if (!myenemyScript.animating)
        {
            myenemyScript.animating = true;
            anim.Play(damagedName);
        }
    }

    public void PlayDamagedCritAnimation()
    {
        
        if (!myenemyScript.animating)
        {
            myenemyScript.animating = true;
            anim.Play(damagedCritName);
        }
    }
    public void PlayDefeatAnimation()
    {
        //Debug.Log(transform.name + ": Playing Death Animation");

        myenemyScript.animating = true;
        anim.Play(defeatName);

    }
    //public void PlayIdleAnimation()
    //{
    //    //Debug.Log(transform.name + ": Playing Idle Animation");
    //    //anim.Play("Base Layer.Idle");
    //}

}

