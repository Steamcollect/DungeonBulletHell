using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    public override void OnMove()
    {
        transform.position = transform.position + transform.up * moveSpeed * Time.deltaTime;
    }

    public override void OnCollision(GameObject hit)
    {
        hit.GetComponent<EntityHealth>().TakeDamage(attackDamage, transform.position);
        Destroy(gameObject, .01f);
    }
}