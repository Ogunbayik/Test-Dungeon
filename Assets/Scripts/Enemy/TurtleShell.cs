using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : EnemyBase
{
    void Start()
    {
        Initialize(this);
    }
    void Update()
    {
        UpdateBase(this);
    }
}
