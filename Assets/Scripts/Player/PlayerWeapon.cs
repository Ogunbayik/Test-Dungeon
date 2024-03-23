using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        ColliderActivate(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var weaponDamage = 20;
        var enemy = other.gameObject.GetComponent<IDamageable>();

        if (enemy != null)
            enemy.TakeDamage(weaponDamage);
    }

    public void ColliderActivate(bool isActive)
    {
        boxCollider.enabled = isActive;
    }
    
}
