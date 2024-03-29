using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public float chunkRange;
    public Transform playerTransform;

    public List<Entity> entitys = new List<Entity>();

    public static EntityManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        entitys.RemoveAll(x => x == null);
        for (int i = 0; i < entitys.Count; i++)
        {
            entitys[i].OnUpdate();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerTransform.position, chunkRange);
    }
}