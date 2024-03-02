using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    private void Awake()
    {
        Initialize(this);
    }
    private void Update()
    {
        UpdateBase(this);
    }



}
