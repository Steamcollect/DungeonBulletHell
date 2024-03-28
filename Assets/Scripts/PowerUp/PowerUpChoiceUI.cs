using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpChoiceUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameTxt, descriptionTxt;
    [SerializeField] Image powerUpVisual;

    Image backgroundImage;
    public Sprite commonSprite, rareSprite, epicSprite, legendarySprite;

    PowerUpData powerUp;
    [HideInInspector] public PowerUpManager powerUpManager;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void SetChoiceVisual(PowerUpData powerUp)
    {
        this.powerUp = powerUp;

        nameTxt.text = powerUp.powerUpName;
        descriptionTxt.text = powerUp.powerUpDescription;
        powerUpVisual.sprite = powerUp.powerUpVisual;

        switch (powerUp.rarity)
        {
            case PowerUpRarity.Common:
                backgroundImage.sprite = commonSprite;
                break;
            
            case PowerUpRarity.Rare:
                backgroundImage.sprite = rareSprite;
                break;
            
            case PowerUpRarity.Epic:
                backgroundImage.sprite = epicSprite;
                break;
            
            case PowerUpRarity.Legendary:
                backgroundImage.sprite = legendarySprite;
                break;
        }
    }

    public void SelectButton()
    {
        powerUpManager.SelectPowerUp(powerUp);
    }
}