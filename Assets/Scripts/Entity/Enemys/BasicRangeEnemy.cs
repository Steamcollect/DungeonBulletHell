using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangeEnemy : Enemy
{
    [Header("Combat references")]
    public float bulletSpeed;

    public Transform attackPoint;
    public GameObject bulletPrefab;

    public float attackRange;

    public Transform handParent;
    float angle;
    Vector2 lookDir;

    Vector2 velocity = Vector2.zero;
    
    public override void OnUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceFromPlayer < attackRange)
        {
            velocity = Vector2.zero;
            if(canAttack) Attack();
        }
        else
        {
            transform.position = Vector2.SmoothDamp(transform.position, playerTransform.position, ref velocity, distanceFromPlayer / moveSpeed);
        }

        SetHandVisual();
    }

    public override void Attack()
    {
        StartCoroutine(AttackCooldown());

        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation).GetComponent<Bullet>();
        
        bullet.playerTransform = playerTransform;
        bullet.chunkRange = EntityManager.instance.chunkRange;

        bullet.moveSpeed = bulletSpeed;
        bullet.attackDamage = attackDamage;
        bullet.targetTag = playerTransform.tag;
        bullet.targetLayer = targetLayer;

        EntityManager.instance.entitys.Add(bullet);
    }


    void SetHandVisual()
    {
        lookDir = playerTransform.position - transform.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handParent.rotation = Quaternion.Euler(0, 0, angle);
    }
}