using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpManager : MonoBehaviour
{
    [Header("Power up references")]
    [SerializeField] List<PowerUpData> powerUpAvailable = new List<PowerUpData>();
    Dictionary<PowerUpData, int> powerUpUtilisationCount = new Dictionary<PowerUpData, int>();

    [Header("Choice UI references")]
    List<PowerUpChoiceUI> choicesUI = new List<PowerUpChoiceUI>();
    [SerializeField] GameObject choiceUiParent;
    [SerializeField] GameObject choiceUiGO;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            AddPowerUpUI();
        }

        for (int i = 0; i < powerUpAvailable.Count; i++)
        {
            powerUpUtilisationCount.Add(powerUpAvailable[i], 0);
        } 
    }

    public void SetPowerUpChoices()
    {
        List<PowerUpData> currentsPowerUp = new List<PowerUpData>(powerUpAvailable);

        for (int i = 0; i < choicesUI.Count; i++)
        {
            PowerUpData current = null;
            int rnd = Random.Range(0, 100);

            switch (rnd)
            {
                case <= 5:
                    // Legendary
                    currentsPowerUp = powerUpAvailable.Where(elem => elem.rarity == PowerUpRarity.Legendary).ToList();
                    current = currentsPowerUp.GetRandom();
                    choicesUI[i].SetChoiceVisual(current);
                    break;

                case <= 15:
                    // Epic
                    currentsPowerUp = powerUpAvailable.Where(elem => elem.rarity == PowerUpRarity.Epic).ToList();
                    current = currentsPowerUp.GetRandom();
                    choicesUI[i].SetChoiceVisual(current);
                    break;

                case <= 40:
                    // Rare
                    currentsPowerUp = powerUpAvailable.Where(elem => elem.rarity == PowerUpRarity.Rare).ToList();
                    current = currentsPowerUp.GetRandom();
                    choicesUI[i].SetChoiceVisual(current);
                    break;

                default:
                    // Common
                    currentsPowerUp = powerUpAvailable.Where(elem => elem.rarity == PowerUpRarity.Common).ToList();
                    current = currentsPowerUp.GetRandom();
                    choicesUI[i].SetChoiceVisual(current);
                    break;
            }

            currentsPowerUp = new List<PowerUpData>(powerUpAvailable);
            currentsPowerUp.Remove(current);
        }

        choiceUiParent.SetActive(true);
    }

    public void SelectPowerUp(PowerUpData powerUp)
    {
        powerUpUtilisationCount[powerUp]++;
        if (powerUp.maxUtilisation <= powerUpUtilisationCount[powerUp]) RemoverPowerUp(powerUp);

        for (int i = 0; i < powerUp.powerUpUnlockable.Length; i++) addPowerUpToList(powerUp.powerUpUnlockable[i]);

        choiceUiParent.SetActive(false);

        // Set statistics
        switch (powerUp.powerUpType)
        {
            case PowerUpType.Exploding:
                break;

            default:
                break;
        }
    }

    void addPowerUpToList(PowerUpData powerUp)
    {
        powerUpAvailable.Add(powerUp);
        powerUpUtilisationCount.Add(powerUp, 0);
    }
    void RemoverPowerUp(PowerUpData powerUp)
    {
        powerUpAvailable.Remove(powerUp);
        powerUpUtilisationCount.Remove(powerUp);
    }
    void AddPowerUpUI()
    {
        PowerUpChoiceUI choiceUI = Instantiate(choiceUiGO, choiceUiParent.transform).GetComponent<PowerUpChoiceUI>();
        choiceUI.powerUpManager = this;
        choicesUI.Add(choiceUI);
    }
}