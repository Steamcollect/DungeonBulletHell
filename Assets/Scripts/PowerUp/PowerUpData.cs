using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power up", menuName = "Game Data/Power up")]
public class PowerUpData : ScriptableObject
{
    public string powerUpName;
    [TextArea] public string powerUpDescription;
    public Sprite powerUpVisual;

    public PowerUpRarity rarity;

    public int maxUtilisation;
}

[System.Serializable]
public enum PowerUpRarity
{
    Common, // 60% (100)
    Rare, // 25% (40)
    Epic, // 10% (15)
    Legendary // 5% (5)
}