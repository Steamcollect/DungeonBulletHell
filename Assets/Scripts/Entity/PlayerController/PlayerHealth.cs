using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : EntityHealth
{
    public Slider healthBarSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        SetHealthBarVisual();
    }

    public override void OnHeal()
    {
        SetHealthBarVisual();
    }

    public override void OnTakeDamage()
    {
        SetHealthBarVisual();
    }

    public override void OnDie()
    {
        
    }

    void SetHealthBarVisual()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }
}