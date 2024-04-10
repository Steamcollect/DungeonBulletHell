using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletUpgrade : MonoBehaviour
{
    public LayerMask targetLayer;

    public abstract void OnUpdate();
    public abstract void OnCollision(GameObject hit);
}