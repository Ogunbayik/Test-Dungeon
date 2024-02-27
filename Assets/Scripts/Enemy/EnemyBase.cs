using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float moveRange;
    [SerializeField] protected float maxWaitTimer;

    protected float waitTimer;
    protected float maximumX;
    protected float minimumX;
    protected float maximumZ;
    protected float minimumZ;

    private Vector3 desiredPosition;
    private Vector3 randomPosition;

    protected bool isWalk = false;
    private bool isFirst = true;
    #region Movement Types
    protected void MovementBetweenTwoPoints(Vector3 firstPos, Vector3 secondPos)
    {
        var distanceBetweenDesiredPos = Vector3.Distance(transform.position, desiredPosition);

        if(isFirst)
        {
            desiredPosition = firstPos;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, movementSpeed * Time.deltaTime);

            if (distanceBetweenDesiredPos <= 0.1f)
            {
                //Waiting
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    //Going other position
                    waitTimer = maxWaitTimer;
                    isFirst = false;
                    desiredPosition = secondPos;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, movementSpeed * Time.deltaTime);

            if (distanceBetweenDesiredPos <= 0.1f)
            {
                //Waiting
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    //Going other position
                    waitTimer = maxWaitTimer;
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
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                waitTimer = maxWaitTimer;
                randomPosition = GetRandomPosition();
                Debug.Log(randomPosition);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, randomPosition, movementSpeed * Time.deltaTime);
    }

    private Vector3 GetRandomPosition()
    {
        var randomX = Random.Range(minimumX, maximumX);
        var randomZ = Random.Range(minimumZ, maximumZ);

        randomPosition = new Vector3(randomX, 0f, randomZ);
        return randomPosition;
    }

    #endregion

}
