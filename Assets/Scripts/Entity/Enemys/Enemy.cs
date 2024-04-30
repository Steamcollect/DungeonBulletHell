using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityState))]
public abstract class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    [HideInInspector] public float chunkRange;

    public string targetTag;
    public LayerMask targetLayer;

    [Header("Statistics references")]
    public int attackDamage;
    public float moveSpeed;
    public float attackCooldown;

    [HideInInspector] public bool canAttack = true;

    [HideInInspector] public EntityState entityState;

    private void Awake()
    {
        entityState = GetComponent<EntityState>();
    }

    public abstract void OnUpdate();

    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}