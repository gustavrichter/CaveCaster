using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireAttackScript : SpellAttackScript
{
    public override void AttackEnemy(float basedamage, GameObject enemy)
    {
        AkSoundEngine.PostEvent("Spell_Fire", gameObject);
        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeFireDamage(basedamage, m_element);
    }
}
