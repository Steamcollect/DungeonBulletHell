using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpChoiceUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameTxt, descriptionTxt;
    [SerializeField] Image powerUpVisual;

    PowerUpData powerUp;
    [HideInInspector] public PowerUpManager powerUpManager;

    public void SetChoiceVisual(PowerUpData powerUp)
    {
        this.powerUp = powerUp;

        nameTxt.text = powerUp.powerUpName;
        descriptionTxt.text = powerUp.powerUpDescription;
        powerUpVisual.sprite = powerUp.powerUpVisual;
    }

    public void SelectButton()
    {
        powerUpManager.SelectPowerUp(powerUp);
    }
}