using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour
{
    Animator anim;
    public string attackName;
    public string damagedName;
    public string defeatName;
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log(transform.name + ": Animator not found");
        }
    }

    public void PlayAttackAnimation()
    {
        Debug.Log(transform.name + ": Playing Attack Animation");
        anim.Play(attackName);
    }

    public void PlayDamagedAnimation()
    {
        Debug.Log(transform.name + ": Playing Damaged Animation");
        anim.Play(damagedName);
    }

    public void PlayDefeatAnimation()
    {
        Debug.Log(transform.name + ": Playing Death Animation");
        anim.Play(defeatName);
    }
    public void PlayIdleAnimation()
    {
        //Debug.Log(transform.name + ": Playing Idle Animation");
        //anim.Play("Base Layer.Idle");
        anim.Play(defeatName);
    }

}

