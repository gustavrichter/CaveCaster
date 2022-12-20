using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeNatureScript : EnemySlimeScript
{
    // Start is called before the first frame update

    protected override void Init()
    {
        base.Init();
        elementResistances[nature] = 2;
        elementResistances[ice] = 0.5f;
    }
}
