using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string WALK_ANIMATION_PARAMETER = "isWalk";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        var isWalk = GetComponentInParent<EnemyBase>().GetIsWalk();

        if (isWalk)
            WalkAnimation(true);
        else
            WalkAnimation(false);
    }
    public void WalkAnimation(bool isWalk)
    {
        animator.SetBool(WALK_ANIMATION_PARAMETER, isWalk);
    }

}
