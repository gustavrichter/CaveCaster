using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNatureAttackScript : SpellAttackScript
{
    
 
    public override void AttackEnemy(float basedamage, GameObject enemy)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerScript playerScript = playerObj.GetComponent<PlayerScript>();
        //playerScript.AddHealthPotion();
        playerScript.HealPlayer(10.0f);

        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeNatureDamage(basedamage, m_element);

    }
}
