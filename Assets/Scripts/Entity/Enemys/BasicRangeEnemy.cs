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
        float distanceFromPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceFromPlayer < attackRange)
        {
            velocity = Vector2.zero;
            if(canAttack) Attack();
        }
        else
        {
            transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, distanceFromPlayer / moveSpeed);
        }

        SetHandVisual();
    }

    public override void Attack()
    {
        StartCoroutine(AttackCooldown());

        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation).GetComponent<Bullet>();
        bullet.moveSpeed = bulletSpeed;
        bullet.attackDamage = attackDamage;
        bullet.targetTag = targetTag;

        EntityManager.instance.bullets.Add(bullet);
    }


    void SetHandVisual()
    {
        lookDir = (Vector2)(target.position - transform.position);

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handParent.rotation = Quaternion.Euler(0, 0, angle);
    }
}