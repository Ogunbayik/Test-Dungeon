using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image heartImage;
    [SerializeField] private Text healthText;
    [SerializeField] private int maxHealth;

    private int currentHealth;
    private float healthRate;
    void Start()
    {
        currentHealth = maxHealth;
        healthRate = currentHealth / maxHealth;
        heartImage.fillAmount = healthRate;
        healthText.text = currentHealth.ToString();
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = currentHealth.ToString();
        healthRate = (float)currentHealth / maxHealth;
        heartImage.fillAmount = healthRate;
    }
}
