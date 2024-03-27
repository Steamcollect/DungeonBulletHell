using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    float maxXp, currentXp;
    int level = 1;

    public Slider xpSlider;

    private void Start()
    {
        maxXp = 0.04f * (level ^ 3) + 0.8f * (level ^ 2) + 2 * level;
        SetXpBarVisual();
    }

    private void Update()
    {
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

            xpSlider.maxValue = maxXp;
            xpSlider.value = currentXp;
            TakeXP(xpRemining);
        }

        SetXpBarVisual();
    }

    void SetXpBarVisual()
    {
        xpSlider.maxValue = maxXp;
        xpSlider.value = currentXp;
    }
}