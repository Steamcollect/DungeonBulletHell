using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Statistics references")]
    public int attackDamage;
    public float moveSpeed;
    public float attackCooldown;

    [HideInInspector]public bool canAttack = true;

    public override void OnUpdate()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > chunkRange)
        {
            float posX = (transform.position.x - playerTransform.position.x) * .9f * -1;
            float posY = (transform.position.y - playerTransform.position.y) * .9f * -1;
            transform.position = playerTransform.position + new Vector3(posX, posY);
        }
    }

    public abstract void Attack();

    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}