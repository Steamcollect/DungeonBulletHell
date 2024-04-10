using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealth : MonoBehaviour
{
    public float maxHealth, currentHealth;

    public void Heal(int healthGiven)
    {
        currentHealth += healthGiven;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;

        OnHeal();
    }
    public abstract void OnHeal();

    public void TakeMaxHealth(float healthGivenPourcentage)
    {
        maxHealth *= healthGivenPourcentage;
        currentHealth *= healthGivenPourcentage;

        OnHeal();
    }

    public void TakeDamage(int damageTaken, Vector2 attackerPos)
    {
        currentHealth -= damageTaken;
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        PopUpManager.instance.HitPopUp(transform.position, attackerPos.x < transform.position.x ? false : true, damageTaken.ToString());

        if (currentHealth <= 0) OnDie();
        
        OnTakeDamage();
    }
    public abstract void OnTakeDamage();
    public abstract void OnDie();
}