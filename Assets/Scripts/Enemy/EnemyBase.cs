using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected enum MovementType
    {
        TwoPoints,
        RandomPosition
    }

    [SerializeField] protected MovementType movementType;
    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected PlayerAttackController player;

    private float waitTimer;
    private float maximumX;
    private float minimumX;
    private float maximumZ;
    private float minimumZ;

    private Vector3 firstPosition;
    private Vector3 secondPosition;
    private Vector3 desiredPosition;
    private Vector3 randomPosition;

    private int currentHealth;

    private bool isWalk = false;
    private bool isFirst = true;
    private bool xDirection;

    public bool canHit = true;

    protected void Initialize(EnemyBase enemyBase)
    {
        if (enemyBase.movementType == MovementType.TwoPoints)
            InitialTwoDirection(enemyBase);
        else if (enemyBase.movementType == MovementType.RandomPosition)
            InitialRandomPosition(enemyBase);

        player = FindObjectOfType<PlayerAttackController>();
        currentHealth = enemyBase.enemySO.maxHealth;
        waitTimer = enemyBase.enemySO.maxWaitTimer;

        player.OnHitEnemy += Player_OnHitEnemy;
    }

    private void Player_OnHitEnemy(object sender, int e)
    {
        if (currentHealth > e)
        {
            TakeDamage(e);
            Debug.Log("Hitted");
            canHit = false;
        }
        else
        {
            Debug.Log("Dead");
            canHit = false;
        }
    }

    private void InitialTwoDirection(EnemyBase enemyBase)
    {
        desiredPosition = enemyBase.transform.localPosition;

        var randomValue = Random.value;
        if (randomValue > 0.5)
            xDirection = false;
        else
            xDirection = true;

        if (xDirection)
        {
            firstPosition = enemyBase.transform.localPosition + new Vector3(enemyBase.enemySO.moveRange, 0f, 0f);
            secondPosition = enemyBase.transform.localPosition - new Vector3(enemyBase.enemySO.moveRange, 0f, 0f);
        }
        else
        {
            firstPosition = enemyBase.transform.localPosition + new Vector3(0f, 0f, enemyBase.enemySO.moveRange);
            secondPosition = enemyBase.transform.localPosition - new Vector3(0f, 0f, enemyBase.enemySO.moveRange);
        }
    }

    private void InitialRandomPosition(EnemyBase enemyBase)
    {
        maximumX = enemyBase.transform.localPosition.x + enemyBase.enemySO.moveRange;
        minimumX = enemyBase.transform.localPosition.x - enemyBase.enemySO.moveRange;
        maximumZ = enemyBase.transform.localPosition.z + enemyBase.enemySO.moveRange;
        minimumZ = enemyBase.transform.localPosition.z - enemyBase.enemySO.moveRange;
        randomPosition = enemyBase.transform.localPosition;
    }

    protected void Movement(EnemyBase enemyBase)
    {
        if (enemyBase.movementType == MovementType.TwoPoints)
            MovementBetweenTwoPoints(firstPosition, secondPosition);
        else if (enemyBase.movementType == MovementType.RandomPosition)
            MovementRandomPosition();
    }

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

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public bool GetIsWalk()
    {
        return isWalk;
    }

}
