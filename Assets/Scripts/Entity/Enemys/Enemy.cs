using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Statistics references")]
    public int attackDamage;
    public float moveSpeed;
    public float attackCooldown;

    [HideInInspector] public string targetTag = "Player";

    [HideInInspector]public bool canAttack = true;

    public Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void Attack();

    public abstract void OnUpdate();

    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}