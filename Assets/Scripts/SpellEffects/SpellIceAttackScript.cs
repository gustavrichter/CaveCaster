using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIceAttackScript : SpellAttackScript
{
    public override void AttackEnemy(float baseDamage, GameObject enemy)
    {
        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeIceDamage(baseDamage, m_element);
    }
}
