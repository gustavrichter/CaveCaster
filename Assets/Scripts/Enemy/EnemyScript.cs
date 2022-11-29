using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class EnemyScript : MonoBehaviour
{
    public int m_id;
    protected GameObject player;
    protected PlayerScript playerScript;
    protected float coolDown;
    protected float health;
    protected float damage;
    protected int fire = 0;
    protected int water = 1;
    protected int ice = 2;
    protected int nature = 3;
    protected int shock = 4;
    protected float[] elementResistances = {1, 1, 1, 1, 1};//fire, water, ice, nature, shock (no resistances)
    protected bool alive;

    public Action<int> EnemyDeath = delegate { };
    
    protected virtual void Awake()
    {
        
        Init();
    }
    protected virtual void Init()
    {
        damage = 10;
        coolDown = 4;
        health = 100;
        alive = true;

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        if (!playerScript)
            Debug.Log("PlayerScript not found");
    }
    protected virtual void Update()
    {
        coolDown -= Time.deltaTime;
        if(coolDown<= 0)
        {
            coolDown = 15.0f;
            Attack();
        }
        if (!alive)
        {
            Debug.Log(this.transform.name + " has been slayn.");
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage, int element) {

        health -= (damage/elementResistances[element]);

        if (elementResistances[element] < 1)//if crit
            Debug.Log(gameObject.name + ": Extra Outch! " + health);
        else if(elementResistances[element] > 1)
            Debug.Log(gameObject.name + ": Not so Outch! " + health);
        else
            Debug.Log(gameObject.name + ": Outch! " + health);

        if (health<= 0)
        {
            alive = false;
            EnemyDeath(m_id);
        }
    }
    public abstract void Attack();

}
