using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWaterAttackScript :SpellAttackScript
{

    public override void AttackEnemy(float baseDamage, GameObject enemy)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerScript playerScript = playerObj.GetComponent<PlayerScript>();
        playerScript.AddInkBottle();

        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeWaterDamage(baseDamage, m_element);
    }

}
