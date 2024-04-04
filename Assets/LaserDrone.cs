using UnityEngine;

public class LaserDrone : Entity
{
    public float moveSpeed, attackDamagePerSeconde;
    public float attackTime, attackCouldown;

    public float targetDetectionRange, movePosRadius;

    public LayerMask targetLayer;

    Transform target;
    Vector2 lookDir;
    float angle;

    Vector2 movePos;
    Vector2 velocity = Vector2.zero;

    public override void OnUpdate()
    {
        if (target == null || Vector2.Distance(playerTransform.position, target.position) > targetDetectionRange) target = GetAttackTarget();
        else
        {
            // Attack

        }

        Move();
        UpdateVisual();
    }

    void Move()
    {
        float dist = Vector2.Distance(transform.position, (Vector2)playerTransform.position + movePos);

        if (dist < .1f) movePos = GetMovePos();

        transform.position = Vector2.SmoothDamp(transform.position, (Vector2)playerTransform.position + movePos, ref velocity, dist / moveSpeed);
    }

    Vector2 GetMovePos()
    {
        return (Vector2)playerTransform.position + (Random.insideUnitCircle.normalized * movePosRadius);
    }
    Transform GetAttackTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, targetDetectionRange, targetLayer);
        
        Transform tmp = null;
        float minDist = 999;
        for (int i = 0; i < targets.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, targets[i].transform.position);

            if (dist <= targetDetectionRange && dist <= minDist) tmp = targets[i].transform;
        }
        return tmp;
    }

    public void UpdateVisual()
    {
        lookDir = target == null ? movePos : target.position - transform.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);
        Gizmos.DrawWireSphere(playerTransform.position, movePosRadius);
    }
}