using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangeEnemy : Enemy
{
    [Header("Combat references")]
    public float bulletSpeed;

    public Transform attackPoint;

    public float attackRange;

    public Transform handParent;
    float angle;
    Vector2 lookDir;

    Vector2 velocity = Vector2.zero;
    
    public override void OnUpdate()
    {
        if (entityState.currentState == EntityStateExisting.Stun) return;

        float distanceFromPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceFromPlayer < attackRange)
        {
            velocity = Vector2.zero;
            if(canAttack) Attack();
        }
        else
        {
            transform.position = Vector2.SmoothDamp(transform.position, playerTransform.position, ref velocity, distanceFromPlayer / entityState.CalculateMoveSpeed(moveSpeed));
        }

        SetHandVisual();
    }

    public void Attack()
    {
        StartCoroutine(AttackCooldown());

        Bullet bullet = EntityManager.instance.SpawnBullet();
        bullet.transform.position = attackPoint.position;
        bullet.transform.rotation = attackPoint.rotation;

        bullet.playerTransform = playerTransform;
        bullet.chunkRange = EntityManager.instance.chunkRange;

        bullet.moveSpeed = bulletSpeed;
        bullet.attackDamage = attackDamage;
        bullet.targetTag = playerTransform.tag;
        bullet.targetLayer = targetLayer;

        bullet.graphics.color = new Color(1, .1f, .1f, 1);

        EntityManager.instance.bullets.Add(bullet);
    }

    void SetHandVisual()
    {
        lookDir = playerTransform.position - transform.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handParent.rotation = Quaternion.Euler(0, 0, angle);
    }
}