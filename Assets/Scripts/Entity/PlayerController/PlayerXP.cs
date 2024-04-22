using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerXP : MonoBehaviour
{
    float maxXp, currentXp;
    int level = 1;

    public Transform xpBar;
    public TMP_Text levelTxt;

    PowerUpManager powerUpManager;

    public static PlayerXP instance;

    private void Awake()
    {
        powerUpManager = FindFirstObjectByType<PowerUpManager>();

        instance = this;
    }

    private void Start()
    {
        maxXp = 0.04f * (level ^ 3) + 0.8f * (level ^ 2) + 2 * level;
        SetXpBarVisual();
    }

    private void Update()
    {
        if (GameStateManager.instance.gameState != GameState.Gameplay) return;

        if (Input.GetKeyDown(KeyCode.P)) TakeXP(5);
    }

    public void TakeXP(float xpGiven)
    {
        currentXp += xpGiven;

        if (currentXp >= maxXp)
        {
            float xpRemining = (maxXp - currentXp) * -1;

            level++;
            currentXp = 0;

            maxXp = 0.04f * (level ^ 3) + 0.8f * (level ^ 2) + 2 * level;

            powerUpManager.SetPowerUpChoices();

            TakeXP(xpRemining);
        }

        SetXpBarVisual();
    }

    void SetXpBarVisual()
    {
        xpBar.DOScaleX(currentXp / maxXp, .15f);
        levelTxt.text = "lvl " + level.ToString();
    }
}