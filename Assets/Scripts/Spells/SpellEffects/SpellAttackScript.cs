using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellAttackScript : MonoBehaviour
{
    protected float m_baseDmg;
    protected int m_element;
    protected GameObject m_enemy;
    protected EnemyScript m_enemyScript;
    protected EnemyTakeDamageScript m_enemyDamageScript;
    [SerializeField]
    protected SpellScript m_mySpellScript;

    private void Start()
    {
        m_baseDmg = m_mySpellScript.getDamage();
        m_element = m_mySpellScript.getElement();
    }

    public abstract void AttackEnemy(float baseDamage, GameObject enemy);

    protected void StandardAttack()
    {
        m_enemyDamageScript.TakeNormalDamage(m_baseDmg, m_element);
    }
  
    protected void GetEnemyScripts(GameObject enemy)
    {
        m_enemyDamageScript = enemy.GetComponent<EnemyTakeDamageScript>();
    }
}
