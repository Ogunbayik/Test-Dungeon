using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase ,IDamageable
{
    public void TakeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
            isInvulnerable = true;
            waitTimer = enemySO.maxWaitTimer / 2;
        }
        else
        {
            Debug.Log("Dead");
            isInvulnerable = true;
        }

    }

    private void Awake()
    {
        Initialize(this);
    }
    private void Update()
    {
        UpdateBase(this);
    }



}
