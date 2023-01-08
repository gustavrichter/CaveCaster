using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWaterAttackScript :SpellAttackScript
{

    public override void AttackEnemy(float baseDamage, GameObject enemy)
    {
        //GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //PlayerScript playerScript = playerObj.GetComponent<PlayerScript>();
        //playerScript.AddInkBottle();

        GameObject bookObject = GameObject.FindGameObjectWithTag("MagicBook");
        MagicBookScript bookScript = bookObject.GetComponent<MagicBookScript>();
        bookScript.SetUseInk();

        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeWaterDamage(baseDamage, m_element);
    }

}
