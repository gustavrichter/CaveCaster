using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIceAttackScript : SpellAttackScript
{
    public override void AttackEnemy(float baseDamage, GameObject enemy)
    {
        AkSoundEngine.PostEvent("Spell_Ice", gameObject);

        GetEnemyScripts(enemy);
        m_enemyDamageScript.TakeIceDamage(baseDamage, m_element);
    }
}
