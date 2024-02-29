using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackController : MonoBehaviour
{
    public event EventHandler<int> OnHitEnemy;
    public event EventHandler OnAttack;

    private PlayerController playerController;

    [SerializeField] private int attackDamage;
    [SerializeField] private float attackDelay;
    [SerializeField] private Transform weaponCheckPoint;
    [SerializeField] private float weaponRadius;
    [SerializeField] private LayerMask enemyLayer;

    private float attackTimer;

    private bool canAttack;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        attackTimer = 0;
    }

    void Update()
    {
        HandleAttack();
    }

    private void FixedUpdate()
    {
        var enemyArray = Physics.OverlapSphere(weaponCheckPoint.position, weaponRadius, enemyLayer);

        foreach (var enemy in enemyArray)
        {
            if (enemy.GetComponent<EnemyBase>().canHit == true)
                OnHitEnemy?.Invoke(this, attackDamage);
        }

    }

    private void HandleAttack()
    {
        if (attackTimer <= 0)
            canAttack = true;
        else
            attackTimer -= Time.deltaTime;

        var attackButton = Input.GetKeyDown(KeyCode.Space);
        var isMove = playerController.GetIsMove();

        if (canAttack && attackButton && !isMove)
        {
            canAttack = false;
            attackTimer = attackDelay;
            OnAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(weaponCheckPoint.position, weaponRadius);
    }

}
