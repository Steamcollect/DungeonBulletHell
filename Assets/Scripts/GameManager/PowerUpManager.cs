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
    [SerializeField] Animator choiceAnim;

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
        GameStateManager.instance.PauseGameState();

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

        if(IsAllChoicesEmpty())
        {
            print("New selection");
            SetPowerUpChoices();
            return;
        }
        bool IsAllChoicesEmpty()
        {
            foreach (var current in choicesUI)
            {
                if (current.gameObject.activeSelf == true) return false;
            }

            return true;
        }

        choiceAnim.SetBool("IsOpen", true);
    }

    public void SelectPowerUp(PowerUpData powerUp)
    {
        choiceAnim.SetBool("IsOpen", false);

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
                OnHandOfGod = true;
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
                playerMovement.moveSpeed *= 1.05f;
                    break;
            case PowerUpType.RuneOfAgilityII:
                playerMovement.moveSpeed *= 1.08f;
                break;
            case PowerUpType.RuneOfAgilityIII:
                playerMovement.moveSpeed *= 1.13f;
                break;
            case PowerUpType.RuneOfAgilityIV:
                playerMovement.moveSpeed *= 1.2f;
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

            case PowerUpType.FrozenMana:
                playerCombat.bulletUpgrades.Add(PowerUpType.FrozenMana);
                break;
            case PowerUpType.FrozenHeart:
                playerCombat.bulletUpgrades.Add(PowerUpType.FrozenHeart);
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

        StartCoroutine(OnAnimationEnd());
    }

    bool OnHandOfGod = false;
    IEnumerator OnAnimationEnd()
    {
        yield return new WaitForSeconds(.5f);

        if (OnHandOfGod)
        {
            choicesCount++;
            AddChoiceUI();
            OnHandOfGod = false;
        }

        StartCoroutine(SetGameplay());
    }

    IEnumerator SetGameplay()
    {
        Time.timeScale = 0;
        GameStateManager.instance.ResumeGameState();

        while (Time.timeScale < 1)
        {
            Time.timeScale += 4*Time.deltaTime;
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