using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : EnemyBase
{
    void Start()
    {
        base.Initialize(this);
    }
    void Update()
    {
        Movement(this);
    }
}
