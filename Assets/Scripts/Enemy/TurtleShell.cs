using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : EnemyBase
{
    private Vector3 firstPosition;
    private Vector3 secondPosition;
    void Start()
    {
        waitTimer = enemySO.maxWaitTimer;

        firstPosition = transform.localPosition + new Vector3(enemySO.moveRange, 0f, 0f);
        secondPosition = transform.localPosition - new Vector3(enemySO.moveRange, 0f, 0f);
        
    }
    void Update()
    {
        MovementBetweenTwoPoints(firstPosition, secondPosition);
    }
}
