using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBulletSchrapnel : Bullet
{
    public GameObject firstEnemy;

    public override void OnMove()
    {
        transform.position = transform.position + transform.up * moveSpeed * Time.deltaTime;
    }

    public override void OnCollision(GameObject hit)
    {
        if(hit != firstEnemy)
        {
            hit.GetComponent<EntityHealth>().TakeDamage(attackDamage);

            Destroy(gameObject, .01f);
        }
    }
}