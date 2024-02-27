using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    private EnemyAnimationController animationController;

    private Vector3 firstPosition;
    private Vector3 secondPosition;

    private void Awake()
    {
        animationController = GetComponentInChildren<EnemyAnimationController>();
        waitTimer = maxWaitTimer;

        maximumX = transform.localPosition.x + moveRange;
        maximumZ = transform.localPosition.z + moveRange;
        minimumX = transform.localPosition.x - moveRange;
        minimumZ = transform.localPosition.z - moveRange;
    }
    void Start()
    {
        firstPosition = transform.localPosition + new Vector3(5f, 0f, 0f);
        secondPosition = transform.localPosition + new Vector3(-5f, 0, 0f);
    }

    private void Update()
    {

        //MovementBetweenTwoPoints(firstPosition, secondPosition);
        MovementRandomPosition();
    }

}
