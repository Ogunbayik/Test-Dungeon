using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    private void Awake()
    {
        base.Initialize(this);
    }
    private void Update()
    {
        base.Movement(this);
    }

}
