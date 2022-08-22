using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IEnemyDamage
{
    public int MaxHealth = 100;

    private int currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    
    public void DamageEnemy(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
