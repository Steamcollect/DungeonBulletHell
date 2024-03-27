using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public int attackDamage;

    public string targetTag;

    public abstract void OnMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            OnCollision(collision.gameObject);
        }
    }
    public abstract void OnCollision(GameObject hit);
}