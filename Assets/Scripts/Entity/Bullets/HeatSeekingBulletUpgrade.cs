using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HeatSeekingBulletUpgrade : BulletUpgrade
{
    public float detectionRange;

    public float rotationSpeed;
    Transform target = null;

    Vector2 lookDir;
    float angle;

    public override void OnUpdate()
    {
        if (target == null) FindTarget();
        else Rotate();
    }

    void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, targetLayer);
        if (colliders.Length > 0)
        {
            angle = transform.rotation.eulerAngles.z;
            target = colliders[0].transform;
        }
    }
    void Rotate()
    {
        lookDir = target.position - transform.position;
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        angle = Mathf.LerpAngle(angle, targetAngle, rotationSpeed);

        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    public override void OnCollision(GameObject hit)
    {
        // Do nothing
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
