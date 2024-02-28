using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemySO enemySO;

    protected float waitTimer;
    protected float maximumX;
    protected float minimumX;
    protected float maximumZ;
    protected float minimumZ;

    protected Vector3 desiredPosition;
    protected Vector3 randomPosition;

    protected bool isWalk = false;
    private bool isFirst = true;

    #region Movement Types
    protected void MovementBetweenTwoPoints(Vector3 firstPos, Vector3 secondPos)
    {
        var distanceBetweenDesiredPos = Vector3.Distance(transform.position, desiredPosition);
        if(isFirst)
        {
            isWalk = true;
            desiredPosition = firstPos;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, enemySO.walkSpeed * Time.deltaTime);

            if (distanceBetweenDesiredPos <= 0.1f)
            {
                //Waiting
                isWalk = false;
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0)
                {
                    //Going other position
                    isWalk = true;
                    waitTimer = enemySO.maxWaitTimer;
                    isFirst = false;
                    desiredPosition = secondPos;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, enemySO.walkSpeed * Time.deltaTime);
            isWalk = true;
            if (distanceBetweenDesiredPos <= 0.1f)
            {
                //Waiting
                isWalk = false;
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0)
                {
                    //Going other position
                    isWalk = true;
                    waitTimer = enemySO.maxWaitTimer;
                    isFirst = true;
                    desiredPosition = firstPos;
                }
            }
        }
        transform.LookAt(desiredPosition);
    }

    protected void MovementRandomPosition()
    {
        var distanceBetweenRandomPosition = Vector3.Distance(transform.position, randomPosition);

        if(distanceBetweenRandomPosition <= 0.1f)
        {
            isWalk = false;
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                isWalk = true;
                waitTimer = enemySO.maxWaitTimer;
                randomPosition = GetRandomPosition();
                Debug.Log(randomPosition);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, randomPosition, enemySO.walkSpeed * Time.deltaTime);
        transform.LookAt(randomPosition);
    }


    private Vector3 GetRandomPosition()
    {
        var randomX = Random.Range(minimumX, maximumX);
        var randomZ = Random.Range(minimumZ, maximumZ);

        randomPosition = new Vector3(randomX, 0f, randomZ);
        return randomPosition;
    }
    #endregion

    public bool GetIsWalk()
    {
        return isWalk;
    }

}
