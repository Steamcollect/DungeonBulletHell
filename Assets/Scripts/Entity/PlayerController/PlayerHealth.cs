using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : EntityHealth
{
    public RectTransform healthBarParent;
    public Transform healthBarVisual, damageBarTemplate;
    public TMP_Text healthTxt;

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
        healthTxt.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        healthBarVisual.DOScaleX(currentHealth / maxHealth, .15f);
    }
}