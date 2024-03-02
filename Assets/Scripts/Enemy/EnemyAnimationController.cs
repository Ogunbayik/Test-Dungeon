using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string WALK_ANIMATION_PARAMETER = "isWalk";
    private const string HIT_ANIMATION_PARAMETER = "isHit";

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

    public void HitAnimation()
    {
        animator.SetTrigger(HIT_ANIMATION_PARAMETER);
    }

}
