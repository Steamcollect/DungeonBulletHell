using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{    
    [Header("Power up references")]
    [SerializeField] List<PowerUpData> powerUpAvailable = new List<PowerUpData>();
    Dictionary<PowerUpData, int> powerUpUtilisationCount = new Dictionary<PowerUpData, int>();

    [Header("Choice UI references")]
    public int choicesCount;
 
    List<PowerUpChoiceUI> choicesUI = new List<PowerUpChoiceUI>();
    [SerializeField] GameObject choiceUiParent;
    [SerializeField] GameObject choiceUiGO;

    [Header("Power up setup references")]
    public GameObject laserGolemGO;

    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCombat = FindFirstObjectByType<PlayerCombat>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    private void Start()
    {
        for (int i = 0; i < choicesCount; i++)
        {
            AddChoiceUI();
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
        choiceUiParent.SetActive(false);

        // Set statistics
        switch (powerUp.powerUpType)
        {
            case PowerUpType.ExplodingBullet:
                break;

            case PowerUpType.LaserGolem:
                laserGolemGO.SetActive(true);
                laserGolemGO.GetComponent<LaserGolem>().Setup();
                break;

            case PowerUpType.HandOfGod:
                choicesCount++;
                AddChoiceUI();
                break;

            case PowerUpType.HeatSeeking_Bullet:
                playerCombat.bulletUpgrades.Add(PowerUpType.HeatSeeking_Bullet);
                break;
            case PowerUpType.HeatSeeking_Bullet_II:
                playerCombat.heatSeekingBulletDetectionRange += 1;
                break;

            case PowerUpType.RuneOfLife:
                playerHealth.TakeMaxHealth(1.15f);
                break;
            case PowerUpType.RuneOfLifeII:
                playerHealth.TakeMaxHealth(1.3f);
                break;
            case PowerUpType.RuneOfLifeIII:
                playerHealth.TakeMaxHealth(1.5f);
                break;
            case PowerUpType.RuneOfLifeIV:
                playerHealth.TakeMaxHealth(1.8f);
                break;

            case PowerUpType.PowerRune:
                playerCombat.attackDamage += 5;
                break;
            case PowerUpType.PowerRuneII:
                playerCombat.attackDamage += 10;
                break;
            case PowerUpType.PowerRuneIII:
                playerCombat.attackDamage += 20;
                break;
            case PowerUpType.PowerRuneIV:
                playerCombat.attackDamage += 30;
                break;
            
            case PowerUpType.RuneOfAgility:
                playerMovement.moveSpeed *= 1.1f;
                    break;
            case PowerUpType.RuneOfAgilityII:
                playerMovement.moveSpeed *= 1.15f;
                break;
            case PowerUpType.RuneOfAgilityIII:
                playerMovement.moveSpeed *= 1.2f;
                break;
            case PowerUpType.RuneOfAgilityIV:
                playerMovement.moveSpeed *= 1.4f;
                break;
        }

        // Remove power up if max utilisation exceeded
        powerUpUtilisationCount[powerUp]++;
        if (powerUp.maxUtilisation <= powerUpUtilisationCount[powerUp])
        {
            // Add new power up to list
            for (int i = 0; i < powerUp.powerUpUnlockable.Length; i++) AddPowerUpToList(powerUp.powerUpUnlockable[i]);

            RemoverPowerUp(powerUp);
        }
    }

    void AddPowerUpToList(PowerUpData powerUp)
    {
        powerUpAvailable.Add(powerUp);
        powerUpUtilisationCount.Add(powerUp, 0);
    }
    void RemoverPowerUp(PowerUpData powerUp)
    {
        powerUpAvailable.Remove(powerUp);
        powerUpUtilisationCount.Remove(powerUp);
    }
    void AddChoiceUI()
    {
        PowerUpChoiceUI choiceUI = Instantiate(choiceUiGO, choiceUiParent.transform).GetComponent<PowerUpChoiceUI>();
        choiceUI.powerUpManager = this;
        choicesUI.Add(choiceUI);
    }
}