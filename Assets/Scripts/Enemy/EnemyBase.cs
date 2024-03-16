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

    protected enum States
    {
        Patrol,
        Chase,
        Dizzy,
        Attack
    }

    [SerializeField] protected MovementType movementType;
    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected PlayerAttackController player;

    protected States currentState;

    private float movementSpeed;
    protected float waitTimer;
    private float maximumX;
    private float minimumX;
    private float maximumZ;
    private float minimumZ;

    private Vector3 firstPosition;
    private Vector3 secondPosition;
    private Vector3 desiredPosition;
    private Vector3 randomPosition;

    protected int currentHealth;

    private bool isWalk = false;
    private bool isFirst = true;
    private bool xDirection;

    public bool isInvulnerable = false;

    protected void Initialize(EnemyBase enemyBase)
    {
        if (enemyBase.movementType == MovementType.TwoPoints)
            InitialTwoDirection(enemyBase);
        else if (enemyBase.movementType == MovementType.RandomPosition)
            InitialRandomPosition(enemyBase);

        player = FindObjectOfType<PlayerAttackController>();
        currentState = States.Patrol;
        currentHealth = enemyBase.enemySO.maxHealth;
        waitTimer = enemyBase.enemySO.maxWaitTimer;

        player.OnHitEnemy += Player_OnHitEnemy;
    }
    private void Player_OnHitEnemy(object sender, int e)
    {
        if (currentHealth > e)
        {
            TakeDamage(e);
            isInvulnerable = true;
            waitTimer = enemySO.maxWaitTimer / 2;
        }
        else
        {
            Debug.Log("Dead");
            isInvulnerable = true;
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

    protected void UpdateBase(EnemyBase enemyBase)
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrolling(enemyBase);
                break;
            case States.Chase:
                Chasing(enemyBase);
                break;
            case States.Dizzy:
                enemyBase.movementSpeed = 0;
                break;
            case States.Attack:
                break;
        }

        CheckInvulnerable(enemyBase);
    }

    private void CheckInvulnerable(EnemyBase enemyBase)
    {
        if (enemyBase.isInvulnerable == true)
        {
            enemyBase.currentState = States.Dizzy;
            movementSpeed = 0;
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                enemyBase.currentState = States.Chase;
                isInvulnerable = false;
                movementSpeed = enemyBase.enemySO.runSpeed;
                waitTimer = enemyBase.enemySO.maxWaitTimer;
            }
        }
    }

    protected void Patrolling(EnemyBase enemyBase)
    {
        movementSpeed = enemyBase.enemySO.walkSpeed;

        if (enemyBase.movementType == MovementType.TwoPoints)
            PatrolBetweenTwoPoints(firstPosition, secondPosition);
        else if (enemyBase.movementType == MovementType.RandomPosition)
            PatrolRandomPosition();

        var distanceBetweenPlayer = Vector3.Distance(enemyBase.transform.position, player.transform.position);
        if (distanceBetweenPlayer <= enemyBase.enemySO.chaseDistance)
        {
            currentState = States.Chase;
        }
    }

    protected void Chasing(EnemyBase enemyBase)
    {
        movementSpeed = enemyBase.enemySO.runSpeed;

        transform.LookAt(player.transform.position);
        transform.position = Vector3.MoveTowards(enemyBase.transform.position, player.transform.position, enemyBase.enemySO.runSpeed * Time.deltaTime);

        var distanceBetweenPlayer = Vector3.Distance(enemyBase.transform.position, player.transform.position);

        if (distanceBetweenPlayer >= enemyBase.enemySO.chaseDistance)
            currentState = States.Patrol;
    }

    #region Movement Types
    protected void PatrolBetweenTwoPoints(Vector3 firstPos, Vector3 secondPos)
    {
        var distanceBetweenDesiredPos = Vector3.Distance(transform.position, desiredPosition);
        if(isFirst)
        {
            isWalk = true;
            desiredPosition = firstPos;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, movementSpeed * Time.deltaTime);

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
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, movementSpeed * Time.deltaTime);
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

    protected void PatrolRandomPosition()
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

        transform.position = Vector3.MoveTowards(transform.position, randomPosition, movementSpeed * Time.deltaTime);
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
