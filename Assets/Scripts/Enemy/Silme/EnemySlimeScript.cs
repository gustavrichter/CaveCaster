using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeScript : EnemyScript
{
    
    //protected override void Init()
    //{
    //    base.Init();
    //    playerScript = player.GetComponent<PlayerScript>();
    //    if (!playerScript)
    //        Debug.Log("PlayerScript not found");
    //}
    public override void Attack() {

        
        int element = 0;
        for (int i = 0; i < elementResistances.Length; i++)
        {
            if (elementResistances[i] == 2) //2 = water element
                element = i;
        }
        //Debug.Log(gameObject.name + "Attack for " + damage + " damage!");
        playerScript.TakeDamage(damage, element);
        
    }


}
