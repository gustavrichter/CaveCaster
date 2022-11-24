using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        coolDown -= Time.deltaTime;
        if(coolDown<= 0)
        {
            coolDown = 15.0f;
            Attack();
        }
    }

    public abstract void Attack();
    protected float coolDown = 15.0f;
    

}
