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
        // Enemys
        enemys.RemoveAll(x => x == null);
        for (int i = 0; i < enemys.Count; i++)
        {
            // Chunk system
            if (Vector2.Distance(enemys[i].transform.position, playerTransform.position) > chunkRange)
            {
                float posX = (enemys[i].transform.position.x - playerTransform.position.x) * .9f * -1;
                float posY = (enemys[i].transform.position.y - playerTransform.position.y) * .9f * -1;
                enemys[i].transform.position = playerTransform.position + new Vector3(posX, posY);
            }

            enemys[i].OnUpdate();
        }
        
        // Bullets
        bullets.RemoveAll(x => x == null);
        for (int i = 0; i < bullets.Count; i++)
        {
            // Chunk system
            if (Vector2.Distance(bullets[i].transform.position, playerTransform.position) > chunkRange) Destroy(bullets[i].gameObject);

            // Call upgrade Update function
            for (int y = 0; y < bullets[i].upgrades.Count; y++) bullets[i].upgrades[y].OnUpdate();
            bullets[i].OnMove();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerTransform.position, chunkRange);
    }
}