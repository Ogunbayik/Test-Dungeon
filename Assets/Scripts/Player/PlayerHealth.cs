using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnHitEnemy;
    public event Action OnDead;
    public event Action<int> OnTakeDamage;

    [SerializeField] private Image heartImage;
    [SerializeField] private Text healthText;
    [SerializeField] private int maxHealth;

    private int currentHealth;
    private float healthRate;
    private bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        healthRate = currentHealth / maxHealth;
        heartImage.fillAmount = healthRate;
        healthText.text = currentHealth.ToString();
    }

    private void OnEnable()
    {
        OnTakeDamage += PlayerHealth_OnTakeDamage;
    }

    private void PlayerHealth_OnTakeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
            healthText.text = currentHealth.ToString();
            healthRate = (float)currentHealth / maxHealth;
            heartImage.fillAmount = healthRate;
        }
        else
        {
            isDead = true;
            currentHealth = 0;
            healthText.text = currentHealth.ToString();
            healthRate = (float)currentHealth / maxHealth;
            heartImage.fillAmount = healthRate;
            OnDead?.Invoke();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyBase>();
        var enemySO = enemy.GetEnemySO();

        if (enemy != null)
        {
            OnHitEnemy?.Invoke();
            OnTakeDamage?.Invoke(enemySO.enemyDamage);
        }
    }
}
