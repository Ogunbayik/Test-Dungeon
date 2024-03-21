using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string ANIMATOR_MOVE_PARAMETER = "isMove";
    private const string ANIMATOR_ATTACK_PARAMETER = "isAttack";
    private const string ANIMATOR_HIT_PARAMETER = "isHit";
    private const string ANIMATOR_DEAD_PARAMETER = "isDead";

    private PlayerAttackController playerAttackController;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAttackController = GetComponentInParent<PlayerAttackController>();
        playerController = GetComponentInParent<PlayerController>();
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void Start()
    {
        playerAttackController.OnAttack += PlayerAttackController_OnAttack;
        playerHealth.OnHitEnemy += PlayerHealth_OnHitEnemy;
        playerHealth.OnDead += PlayerHealth_OnDead;
    }
    void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        var isMove = playerController.GetIsMove();
        if (isMove)
            MoveAnimation(true);
        else
            MoveAnimation(false);
    }

    private void MoveAnimation(bool isMove)
    {
        animator.SetBool(ANIMATOR_MOVE_PARAMETER, isMove);
    }
    private void PlayerAttackController_OnAttack(object sender, System.EventArgs e)
    {
        AttackAnimation(true);
    }
    private void PlayerHealth_OnHitEnemy()
    {
        HitAnimation();
    }
    private void PlayerHealth_OnDead()
    {
        DeadAnimation();
    }

    private void AttackAnimation(bool isAttack)
    {
        animator.SetBool(ANIMATOR_ATTACK_PARAMETER, isAttack);
    }

    private void HitAnimation()
    {
        animator.SetTrigger(ANIMATOR_HIT_PARAMETER);
    }

    private void DeadAnimation()
    {
        animator.SetTrigger(ANIMATOR_DEAD_PARAMETER);
    }


    public void ResetAttackAnimation()
    {
        AttackAnimation(false);
    }
}
