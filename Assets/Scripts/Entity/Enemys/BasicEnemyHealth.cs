using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : EntityHealth
{
    public int xpGiven;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void OnHeal()
    {
        
    }

    public override void OnTakeDamage()
    {
        
    }

    public override void OnDie()
    {
        PlayerXP.instance.TakeXP(xpGiven);
        Destroy(gameObject);
    }
}