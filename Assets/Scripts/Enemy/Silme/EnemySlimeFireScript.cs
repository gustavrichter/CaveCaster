using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeFireScript : EnemySlimeScript
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        elementResistances[fire] = 2;
        elementResistances[water] = 0.5f;
    }
}
