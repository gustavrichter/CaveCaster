using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private EnemyScript myEnemyScript;
    public void TakeNormalDamage(float basedmg,  int element)
    {
        //Debug.Log("TakeNormalDamage: element = " + element);
        //take damage based on spell element and resistances
        myEnemyScript.TakeDamage(basedmg, element);

    }
    public void TakeFireDamage(float basedmg, int element)
    {
        //fire does additional tick damage to enemy
        TakeNormalDamage(basedmg, element);
        StartCoroutine(FireTickDamage(4, 12.0f, element));

    }
    IEnumerator  FireTickDamage(int ticks, float tickdamage, int element)
    {
        if (ticks > 0)
        {
            yield return new WaitForSeconds(1.0f);
            if (myEnemyScript.isAlive())
            {
                //Debug.Log("Doing Fire Tick Damage");
                ticks--;
                TakeNormalDamage(tickdamage, element);
                StartCoroutine(FireTickDamage(ticks, tickdamage, element));
            }
        }
    }

    public void TakeWaterDamage(float basedmg, int element)
    {
        //player gets ink refill
        TakeNormalDamage(basedmg, element);
    }
    public void TakeIceDamage(float basedmg, int element)
    {
        
        TakeNormalDamage(basedmg, element);
        if (myEnemyScript.isAlive())
        {
            myEnemyScript.addCooldown(5.0f);
        }
    }

    public void TakeNatureDamage(float basedmg, int element)
    {
        //player gets heal
        TakeNormalDamage(basedmg, element);
    }

    public void TakeShockDamage(float basedmg, int element)
    {
        //every enemy gets shocked
        TakeNormalDamage(basedmg, element);
    }
}
