using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealth : MonoBehaviour
{
    public int maxHealth, currentHealth;

    public void Heal(int healthGiven)
    {
        currentHealth += healthGiven;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;

        OnHeal();
    }
    public abstract void OnHeal();

    public void TakeMaxHealth(int healthGiven)
    {
        maxHealth += healthGiven;
        currentHealth += healthGiven;

        OnHeal();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        if (currentHealth <= 0) OnDie();
        else OnTakeDamage();
    }
    public abstract void OnTakeDamage();
    public abstract void OnDie();
}