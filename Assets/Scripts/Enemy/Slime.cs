using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    private void Awake()
    {
        randomPosition = transform.localPosition;
        desiredPosition = transform.localPosition;
        
    }
    void Start()
    {
        maximumX = transform.localPosition.x + enemySO.moveRange;
        maximumZ = transform.localPosition.z + enemySO.moveRange;
        minimumX = transform.localPosition.x - enemySO.moveRange;
        minimumZ = transform.localPosition.z - enemySO.moveRange;

        waitTimer = enemySO.maxWaitTimer;
    }

    private void Update()
    {
        MovementRandomPosition();
    }

}
