using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : EnemyBase , IDamageable  
{
    void Start()
    {
        Initialize(this);    }
    void Update()
    {
        UpdateBase(this);
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
            ChangeFillAmount(this);
            isInvulnerable = true;
            waitTimer = enemySO.maxWaitTimer / 2;

            animatorController.HitAnimation();
        }
        else
        {
            Debug.Log("Dead");
            isInvulnerable = true;

            animatorController.DeadAnimation();
        }

    }
}
