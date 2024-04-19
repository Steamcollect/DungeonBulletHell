using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Game Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefabs;
    public float spawningDelay;
}