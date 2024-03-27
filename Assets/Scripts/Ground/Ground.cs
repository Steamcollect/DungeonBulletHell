using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GroundManager groundManager;

    private void Awake()
    {
        groundManager = FindFirstObjectByType<GroundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            groundManager.ChangeGroundPos(transform.position);
        }
    }
}