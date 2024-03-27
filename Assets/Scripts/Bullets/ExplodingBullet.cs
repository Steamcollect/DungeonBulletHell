using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : Bullet
{
    public GameObject bulletPrefab;
    int nbSchrapnel = 32;
    public override void OnMove()
    {
        transform.position = transform.position + transform.up * moveSpeed * Time.deltaTime;
    }

    public override void OnCollision(GameObject hit)
    {
        hit.GetComponent<EntityHealth>().TakeDamage(attackDamage);

        float direction = 0.0f;

        for (int i = 0; i < nbSchrapnel; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, direction)).GetComponent<Bullet>();
            bullet.targetTag = this.targetTag;
            direction += 360f/nbSchrapnel;
            bullet.gameObject.GetComponent<ExplodingBulletSchrapnel>().firstEnemy = hit;
            EntityManager.instance.bullets.Add(bullet);
        }
        
        Destroy(gameObject, .01f);
    }
}