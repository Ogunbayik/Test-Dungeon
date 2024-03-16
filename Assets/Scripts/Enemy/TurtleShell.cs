using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : EnemyBase , IDamageable  
{
    void Start()
    {
        Initialize(this);
    }
    void Update()
    {
        UpdateBase(this);
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            TakeDamage(damage);
            isInvulnerable = true;
            waitTimer = enemySO.maxWaitTimer / 2;
        }
        else
        {
            Debug.Log("Dead");
            isInvulnerable = true;
        }

    }
}
