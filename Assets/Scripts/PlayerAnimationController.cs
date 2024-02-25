using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
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
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);
        
    }
}
