using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShockAttackScript : SpellAttackScript
{

    public override void AttackEnemy(float baseDamage, GameObject enemy)
    {
        GameObject caveObject = GameObject.FindGameObjectWithTag("Cave");
        CaveScript caveScript = caveObject.GetComponent<CaveScript>();
        List<GameObject> enemyObjects= caveScript.getEnemies();
        for (int i = 0; i < enemyObjects.Count; i++)
        {
            GetEnemyScripts(enemyObjects[i]);
            m_enemyDamageScript.TakeShockDamage(baseDamage, m_element);
            
        }
    }

}
