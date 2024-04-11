using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : EntityHealth
{
    [Header("Bar references")]
    public RectTransform healthBarParent;
    public Transform healthBarVisual, damageBar;
    public TMP_Text healthTxt;

    public AnimationCurve healthBarAnimationCurve;
    Vector2 healthBarInitialRect;

    private void Start()
    {
        healthBarInitialRect = healthBarParent.sizeDelta;
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
        damageBar.DOKill();

        // Set health text
        healthTxt.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        
        // Change health bar max value
        healthBarParent.DOSizeDelta(new Vector2(healthBarAnimationCurve.Evaluate(maxHealth), healthBarInitialRect.y), .5f);
        
        // Set damage bar
        if (healthBarVisual.localScale.x > currentHealth / maxHealth) StartCoroutine(ChangeDamageBarVisual());

        // Set healthBar
        healthBarVisual.DOScaleX(currentHealth / maxHealth, .15f);
        if(healthBarVisual.localScale.x < currentHealth / maxHealth) damageBar.DOScaleX(currentHealth / maxHealth, .15f);
    }

    IEnumerator ChangeDamageBarVisual()
    {
        yield return new WaitForSeconds(.5f);
        damageBar.DOScaleX(currentHealth / maxHealth, .25f);
    }
}