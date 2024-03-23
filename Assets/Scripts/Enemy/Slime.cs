using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase , IDamageable
{
    private void Awake()
    {
        Initialize(this);
    }
    private void Update()
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
            SpawnManager.Instance.spawnCount--;

            ChangeFillAmount(this);
            isInvulnerable = true;

            animatorController.DeadAnimation();
        }

    }


}
