using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeWaterScript : EnemySlimeScript
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        elementResistances[water] = 2;
        elementResistances[shock] = 0.5f;
    }
}
