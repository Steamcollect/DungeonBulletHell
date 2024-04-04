using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Transform playerTransform;
    [HideInInspector] public float chunkRange;

    public abstract void OnUpdate();
}