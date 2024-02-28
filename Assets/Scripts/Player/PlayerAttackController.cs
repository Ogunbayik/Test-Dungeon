using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackController : MonoBehaviour
{
    public event EventHandler OnAttack;

    private PlayerController playerController;

    [SerializeField] private float attackDelay;
    [SerializeField] private Transform weaponCheckPosition;
    [SerializeField] private float weaponAttackRadius;
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
}
