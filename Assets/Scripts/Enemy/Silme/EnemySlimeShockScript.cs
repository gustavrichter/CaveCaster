using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeShockScript : EnemySlimeScript
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        elementResistances[shock] = 2;
        elementResistances[nature] = 0.5f;
    }
}
