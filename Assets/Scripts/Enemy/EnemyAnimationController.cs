using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string WALK_ANIMATION_PARAMETER = "isWalk";
    private const string HIT_ANIMATION_PARAMETER = "isHit";
    private const string DEAD_ANIMATION_PARAMETER = "isDead";
    private const string VICTORY_ANIMATION_PARAMETER = "isVictory";

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

    public void DeadAnimation()
    {
        animator.SetTrigger(DEAD_ANIMATION_PARAMETER);
    }

    public void VictoryAnimation()
    {
        animator.SetTrigger(VICTORY_ANIMATION_PARAMETER);
    }

    public void DestroyEnemy()
    {
        var enemy = this.gameObject.GetComponentInParent<EnemyBase>();
        Destroy(enemy.gameObject);
    }

}
