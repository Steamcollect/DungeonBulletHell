using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public float chunkRange;
    public Transform playerTransform;

    public List<Enemy> enemys = new List<Enemy>();
    public List<Bullet> bullets = new List<Bullet>();

    public static EntityManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        enemys.RemoveAll(x => x == null);
        foreach (var enemy in enemys)
        {
            if (Vector2.Distance(enemy.transform.position, playerTransform.position) > chunkRange)
            {
                float posX = (enemy.transform.position.x - playerTransform.position.x) * .9f * -1;
                float posY = (enemy.transform.position.y - playerTransform.position.y) * .9f * -1;
                enemy.transform.position = playerTransform.position + new Vector3(posX, posY);
            }
            else
            {
                enemy.OnUpdate();
            }
        }
        
        bullets.RemoveAll(x => x == null);
        foreach (var bullet in bullets)
        {
            if (Vector2.Distance(bullet.transform.position, playerTransform.position) > chunkRange) Destroy(bullet.gameObject);
            else
            {
                bullet.OnMove();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerTransform.position, chunkRange);
    }
}