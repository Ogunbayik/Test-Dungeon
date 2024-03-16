using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    public event Action<PlayerWeapon> OnTakeDamage;
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
        var weaponDamage = 5;
        var enemy = other.gameObject.GetComponent<IDamageable>();

        if (enemy != null)
            enemy.TakeDamage(weaponDamage);
    }

    public void ColliderActivate(bool isActive)
    {
        boxCollider.enabled = isActive;
    }
    
}
