using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireAttackScript : SpellAttackScript
{
    public override void AttackEnemy(float basedamage, GameObject enemy)
    {
        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeFireDamage(basedamage, m_element);
    }
}
