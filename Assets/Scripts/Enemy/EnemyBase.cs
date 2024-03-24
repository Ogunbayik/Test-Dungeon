using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    protected EnemyAnimationController animatorController;

    [SerializeField] protected Collider enemyCollider;
    [SerializeField] protected MovementType movementType;
    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected PlayerHealth player;
    [SerializeField] protected GameObject healthBar;
    [SerializeField] protected Image fillBar;

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
    protected float healthRate;

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

        currentState = States.Patrol;
        player = FindObjectOfType<PlayerHealth>();
        animatorController = GetComponentInChildren<EnemyAnimationController>();
        enemyCollider = GetComponent<Collider>();
        HealthBarActivate(false);

        currentHealth = enemyBase.enemySO.maxHealth;
        healthRate = (float) currentHealth / enemySO.maxHealth;
        fillBar.fillAmount = healthRate;

        waitTimer = enemyBase.enemySO.maxWaitTimer;
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
            firstPosition = enemyBase.transform.position + new Vector3(enemyBase.enemySO.moveRange, 0f, 0f);
            secondPosition = enemyBase.transform.position - new Vector3(enemyBase.enemySO.moveRange, 0f, 0f);
        }
        else
        {
            firstPosition = enemyBase.transform.position + new Vector3(0f, 0f, enemyBase.enemySO.moveRange);
            secondPosition = enemyBase.transform.position - new Vector3(0f, 0f, enemyBase.enemySO.moveRange);
        }
    }

    private void InitialRandomPosition(EnemyBase enemyBase)
    {
        maximumX = enemyBase.transform.position.x + enemyBase.enemySO.moveRange;
        minimumX = enemyBase.transform.position.x - enemyBase.enemySO.moveRange;
        maximumZ = enemyBase.transform.position.z + enemyBase.enemySO.moveRange;
        minimumZ = enemyBase.transform.position.z - enemyBase.enemySO.moveRange;
        randomPosition = enemyBase.transform.position;
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
                isWalk = false;
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
            enemyBase.enemyCollider.enabled = false;
            movementSpeed = 0;
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                enemyBase.currentState = States.Chase;
                enemyBase.enemyCollider.enabled = true;
                isInvulnerable = false;
                movementSpeed = enemyBase.enemySO.runSpeed;
                waitTimer = enemyBase.enemySO.maxWaitTimer;
            }
        }
    }

    private void Patrolling(EnemyBase enemyBase)
    {
        HealthBarActivate(false);

        movementSpeed = enemyBase.enemySO.walkSpeed;

        if (enemyBase.movementType == MovementType.TwoPoints)
            PatrolBetweenTwoPoints(firstPosition, secondPosition);
        else if (enemyBase.movementType == MovementType.RandomPosition)
            PatrolRandomPosition();

        var isPlayerDead = player.GetIsDead();
        var distanceBetweenPlayer = Vector3.Distance(enemyBase.transform.position, player.transform.position);
        if (distanceBetweenPlayer <= enemyBase.enemySO.chaseDistance && isPlayerDead == false)
        {
            currentState = States.Chase;
        }
    }

    private void Chasing(EnemyBase enemyBase)
    {
        HealthBarActivate(true);

        movementSpeed = enemyBase.enemySO.runSpeed;

        transform.LookAt(player.transform.position);
        transform.position = Vector3.MoveTowards(enemyBase.transform.position, player.transform.position, enemyBase.enemySO.runSpeed * Time.deltaTime);

        var isPlayerDead = player.GetIsDead();
        var distanceBetweenPlayer = Vector3.Distance(enemyBase.transform.position, player.transform.position);

        if (distanceBetweenPlayer >= enemyBase.enemySO.chaseDistance || isPlayerDead == true)
            currentState = States.Patrol;
    }

    private void HealthBarActivate(bool isActive)
    {
        healthBar.gameObject.SetActive(isActive);
    }

    protected void ChangeFillAmount(EnemyBase enemyBase)
    {
        healthRate = (float)currentHealth / enemyBase.enemySO.maxHealth;

        fillBar.fillAmount = healthRate;
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
        isWalk = true;
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

    public bool GetIsWalk()
    {
        return isWalk;
    }

    public EnemySO GetEnemySO()
    {
        return enemySO;
    }
}
