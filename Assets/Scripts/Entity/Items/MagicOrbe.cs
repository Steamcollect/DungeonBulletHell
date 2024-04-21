using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrbe : MonoBehaviour
{
    public int damage;

    public string targetName;
    public LayerMask targetLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetName))
        {
            collision.GetComponent<EntityHealth>().TakeDamage(damage, transform.position);
        }
    }
}