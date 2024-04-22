using System.Collections.Generic;
using System.Collections;
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
    public OrbeCircle circleOrbe;

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
        GameStateManager.instance.gameState = GameState.StopGameAction;

        List<PowerUpData> currentsPowerUp = new List<PowerUpData>(powerUpAvailable);
        List<PowerUpData> currentsRaritySelected = new List<PowerUpData>();

        for (int i = 0; i < choicesUI.Count; i++)
        {
            PowerUpData current = null;
            int rnd = Random.Range(0, 100);

            switch (rnd)
            {
                case <= 5:
                    // Legendary
                    currentsRaritySelected = currentsPowerUp.Where(elem => elem.rarity == PowerUpRarity.Legendary).ToList();
                    SetCurrentChoice();
                    break;

                case <= 15:
                    // Epic
                    currentsRaritySelected = currentsPowerUp.Where(elem => elem.rarity == PowerUpRarity.Epic).ToList();
                    SetCurrentChoice();
                    break;

                case <= 40:
                    // Rare
                    currentsRaritySelected = currentsPowerUp.Where(elem => elem.rarity == PowerUpRarity.Rare).ToList();
                    SetCurrentChoice();
                    break;

                default:
                    // Common
                    currentsRaritySelected = currentsPowerUp.Where(elem => elem.rarity == PowerUpRarity.Common).ToList();
                    SetCurrentChoice();
                    break;
            }

            void SetCurrentChoice()
            {
                if (currentsRaritySelected.Count <= 0)
                {
                    choicesUI[i].gameObject.SetActive(false);
                }
                else
                {
                    choicesUI[i].gameObject.SetActive(true);

                    current = currentsRaritySelected.GetRandom();
                    choicesUI[i].SetChoiceVisual(current);
                }
            }

            currentsPowerUp.Remove(current);
        }

        choiceUiParent.SetActive(true);
    }

    public void SelectPowerUp(PowerUpData powerUp)
    {
        choiceUiParent.SetActive(false);
        StartCoroutine(SetGameState());

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

            case PowerUpType.MagicCircle:
                circleOrbe.gameObject.SetActive(true);
                break;
            case PowerUpType.CircleOrbEvolution:
                circleOrbe.CreateOrbe();
                break;
            case PowerUpType.CircleRangeEvolution:
                circleOrbe.orbesRadius += .5f;
                break;
            case PowerUpType.CircleSpeedEvolution:
                circleOrbe.rotationSpeed += 10;
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

    IEnumerator SetGameState()
    {
        Time.timeScale = 0;
        GameStateManager.instance.gameState = GameState.Gameplay;

        while (Time.timeScale < 1)
        {
            Time.timeScale += 3 * Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 1;
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