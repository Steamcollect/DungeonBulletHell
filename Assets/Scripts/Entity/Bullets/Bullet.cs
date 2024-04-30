using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Transform playerTransform;
    [HideInInspector] public float chunkRange;

    public string targetTag;
    public LayerMask targetLayer;

    public float moveSpeed;
    public int attackDamage;

    [Header("References")]
    public SpriteRenderer graphics;

    [Header("Upgrade references")]
    public List<BulletUpgrade> upgrades = new List<BulletUpgrade>();
    
    [HideInInspector] public float heatSeekingBulletDetectionRange;

    bool canSlow = false;
    bool canFreez = false;

    public abstract void OnMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Collision(collision.gameObject);
            OnCollision(collision.gameObject);
        }
    }
    void Collision(GameObject hit)
    {
        EntityState hitState = hit.GetComponent<EntityState>();

        if (canSlow)
        {
            if (canFreez && hitState.GetState() == EntityStateExisting.Slow) hitState.SetFrozenStun(1.5f);
            else hitState.SetFrozenSlow(3);
        }
    }
    public abstract void OnCollision(GameObject hit);
    
    public void Setup(List<PowerUpType> upgradesType)
    {
        BulletUpgrade[] comps = GetComponents<BulletUpgrade>();
        foreach (BulletUpgrade comp in comps)
        {
            comp.enabled = false;
        }

        StartCoroutine(SetupUpgrade(upgradesType));
    }
    IEnumerator SetupUpgrade(List<PowerUpType> upgradesType)
    {
        yield return new WaitForSeconds(.3f);

        foreach (PowerUpType current in upgradesType)
        {
            switch (current)
            {
                case PowerUpType.HeatSeeking_Bullet:
                    if (TryGetComponent<HeatSeekingBulletUpgrade>(out HeatSeekingBulletUpgrade tmp))
                    {
                        tmp.enabled = true;
                        tmp.detectionRange = heatSeekingBulletDetectionRange;
                        upgrades.Add(tmp);
                    }
                    break;

                case PowerUpType.FrozenMana:
                    canSlow = true;
                    break;
                case PowerUpType.FrozenHeart:
                    canFreez = true;
                    break;                    
            }
        }
    }
}