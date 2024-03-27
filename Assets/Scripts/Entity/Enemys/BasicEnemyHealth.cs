using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : EntityHealth
{
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
        Destroy(gameObject);
    }
}