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
    public bool animating;
    [SerializeField]
    private EnemyAnimationScript animationScript;

    public Action<int> EnemyDeath = delegate { };
    
    protected virtual void Awake()
    {
        
        Init();
        animationScript = GetComponent<EnemyAnimationScript>();
    }
    protected virtual void Init()
    {
        damage = 10;
        coolDown = UnityEngine.Random.Range(4.0f,10.0f);
        health = 100;
        alive = true;
        animating = false;

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        if (!playerScript)
            Debug.Log(transform.name + ": PlayerScript not found");
    }
    protected virtual void Update()
    {
        if(!alive)
        {
            LetEnemyDie();
        }
        else
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0 && !animating)
            {
                coolDown = UnityEngine.Random.Range(4.0f, 10.0f);
                animationScript.PlayAttackAnimation();
            }
        }


    }
    private void LetEnemyDie()
    {
        if (!animating)
        {
            animationScript.PlayDefeatAnimation();
        }
    }
    public void TakeDamage(int damage, int element) {
        //AkSoundEngine.PostEvent("Combat_Enemy_damage", gameObject);
        health -= (damage/elementResistances[element]);

        //if (elementResistances[element] < 1)//if crit
        //    Debug.Log(gameObject.name + ": Extra Outch! " + health);
        //else if(elementResistances[element] > 1)//if resisted
        //    Debug.Log(gameObject.name + ": Not so Outch! " + health);
        //else//normal damage
        //    Debug.Log(gameObject.name + ": Outch! " + health);

        
        if (elementResistances[element] < 1)//if crit
            animationScript.PlayDamagedCritAnimation();
        else
            animationScript.PlayDamagedAnimation();

        if (health <= 0)
        {
            alive = false;
        }


    }
    public abstract void Attack();
    public void EnemyDies()
    {
        //Debug.Log(transform.name + " has been slayn.");
        EnemyDeath(m_id);
    }

    public void finishedAnimating()
    {
        //Debug.Log(transform.name + " finishedAnimating");
        animating = false;
    }
}
    