using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string ANIMATOR_MOVE_PARAMETER = "isMove";

    private PlayerController playerController;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }
    void Start()
    {
        
    }


    void Update()
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
}
