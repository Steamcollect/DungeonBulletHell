using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : Entity
{
    public float moveSpeed;
    public int attackDamage;

    public string targetTag;

    public override void OnUpdate()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > chunkRange) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            OnCollision(collision.gameObject);
        }
    }
    public abstract void OnCollision(GameObject hit);
}