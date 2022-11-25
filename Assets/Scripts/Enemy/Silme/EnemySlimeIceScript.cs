using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeIceScript : EnemySlimeScript
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        elementResistances[ice] = 2;
        elementResistances[fire] = 0.5f;
    }
}
