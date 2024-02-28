using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string ANIMATOR_MOVE_PARAMETER = "isMove";
    private const string ANIMATOR_ATTACK_PARAMETER = "isAttack";

    private PlayerAttackController playerAttackController;
    private PlayerController playerController;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAttackController = GetComponentInParent<PlayerAttackController>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        playerAttackController.OnAttack += PlayerAttackController_OnAttack;
    }

    private void PlayerAttackController_OnAttack(object sender, System.EventArgs e)
    {
        AttackAnimation(true);
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

    private void AttackAnimation(bool isAttack)
    {
        animator.SetBool(ANIMATOR_ATTACK_PARAMETER, isAttack);
    }

    public void ResetAttackAnimation()
    {
        AttackAnimation(false);
    }
}
