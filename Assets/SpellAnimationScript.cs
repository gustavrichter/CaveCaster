using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAnimationScript : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    public string dissolveName;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>() as Animator;
        anim = transform.GetChild(0).GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log(transform.name + ": Animator not found");
        }
    }

    // Update is called once per frame
   public void PlayDissolveAnimation()
    {
        //Debug.Log("Playing Dissolve Animation");
        //anim.Play(dissolveName);
    }
}
