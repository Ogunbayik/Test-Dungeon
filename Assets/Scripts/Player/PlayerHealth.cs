using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    private const string ENVIRONMENT_TAG = "Environment";

    public event Action OnHitEnemy;
    public event Action OnDead;
    public event Action<int> OnTakeDamage;

    private PlayerController playerController;
    private PlayerAttackController playerAttackController;


    [SerializeField] private Image heartImage;
    [SerializeField] private Text healthText;
    [SerializeField] private int maxHealth;

    private int currentHealth;

    private float healthRate;

    private bool isDeath;
    void Start()
    {
        currentHealth = maxHealth;
        healthRate = currentHealth / maxHealth;
        heartImage.fillAmount = healthRate;
        healthText.text = currentHealth.ToString();

        playerController = GetComponent<PlayerController>();
        playerAttackController = GetComponent<PlayerAttackController>();

    }

    private void OnEnable()
    {
        OnTakeDamage += PlayerHealth_OnTakeDamage;
        OnDead += PlayerHealth_OnDead;
    }

    private void PlayerHealth_OnDead()
    {
        StopController();
    }

    private void PlayerHealth_OnTakeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            //HIT
            SetFillBar(damage);
        }
        else
        {
            //DEAD
            isDeath = true;
            SetFillBar(damage);
            currentHealth = 0;
            OnDead?.Invoke();
        }
    }

    private void SetFillBar(int damage)
    {
        currentHealth -= damage;
        healthText.text = currentHealth.ToString();
        healthRate = (float)currentHealth / maxHealth;
        heartImage.fillAmount = healthRate;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ENVIRONMENT_TAG))
            return;

        var enemy = collision.gameObject.GetComponent<EnemyBase>();
        var enemySO = enemy.GetEnemySO();

        if (enemy != null)
        {
            OnHitEnemy?.Invoke();
            OnTakeDamage?.Invoke(enemySO.enemyDamage);
        }
    }

    private void StopController()
    {
        playerController.enabled = false;
        playerAttackController.enabled = false;
    }

    public bool GetIsDead()
    {
        return isDeath;
    }

}
