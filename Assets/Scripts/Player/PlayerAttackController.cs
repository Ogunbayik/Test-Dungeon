using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackController : MonoBehaviour
{
    public event EventHandler<int> OnHitEnemy;
    public event EventHandler OnAttack;

    private PlayerController playerController;
    private PlayerWeapon playerWeapon;

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
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }
    void Start()
    {
        attackTimer = 0;
    }

    void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (attackTimer <= 0)
            canAttack = true;
        else
            attackTimer -= Time.deltaTime;

        if (canAttack)
            playerWeapon.ColliderActivate(false);
        else
            playerWeapon.ColliderActivate(true);

        var attackButton = Input.GetKeyDown(KeyCode.Space);
        var isMove = playerController.GetIsMove();

        if (canAttack && attackButton && !isMove)
        {
            canAttack = false;
            attackTimer = attackDelay;
            OnAttack?.Invoke(this, EventArgs.Empty);
        }
    }

}
