using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAnimationScript : MonoBehaviour
{
    Animator anim;
    public string idleName;
    public string activeName;
    public string implodeName;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>() as Animator;
        if (!anim)
        {
            Debug.Log(transform.name + ": Animator not found");
        }
    }

  

    public void PlayIdleAnimation()
    {
        //Debug.Log(transform.name + ": Playing Idle Animation.");
        //anim.Play(idleName, 0, Random.Range(0.0f, 1.0f));
        anim.Play(idleName, 0, .5f);
    }
}
