using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemys = new List<EnemyData>();
    Dictionary<EnemyData, float> enemysSpawningTimer = new Dictionary<EnemyData, float>();

    public float spawningRadius;

    [Header("Scene references")]
    EntityManager entityManager;
    public Transform playerTransform;

    private void Awake()
    {
        entityManager = GetComponent<EntityManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemysSpawningTimer.Add(enemys[i], 0);
        }
    }

    private void Update()
    {
        foreach (EnemyData enemy in enemys)
        {
            enemysSpawningTimer[enemy] += Time.deltaTime;

            if(enemysSpawningTimer[enemy] >= enemy.spawningDelay)
            {
                enemysSpawningTimer[enemy] = 0;
                SpawnEnemy(enemy);
            }
        }
    }

    void SpawnEnemy(EnemyData enemy)
    {
        Enemy current = Instantiate(enemy.enemyPrefabs).GetComponent<Enemy>();

        current.playerTransform = playerTransform;

        Vector2 randomPointAroundPlayer = (Vector2)playerTransform.position + (Random.insideUnitCircle.normalized * spawningRadius);
        current.transform.position = randomPointAroundPlayer;
                
        entityManager.entitys.Add(current);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, spawningRadius);
    }
}