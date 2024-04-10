using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCombat : Entity
{
    [Header("Statistics references")]
    public float attackCooldown;
    public float bulletSpeed;
    public int attackDamage;

    bool canAttack = true;

    public string targetTag;

    [Header("Combat references")]
    public GameObject bulletPrefab;

    public Transform handParent;
    public Transform attackPoint;

    public Animator handAnim;

    [Header("Upgrade references")]
    public List<PowerUpType> bulletUpgrades = new List<PowerUpType>();

    [HideInInspector] public float heatSeekingBulletDetectionRange = 1;

    float angle;

    Vector2 lookDir;
    Vector2 mousePos;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        EntityManager.instance.entitys.Add(this);
    }

    public override void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canAttack) Attack();

        Rotate();
    }

    void Attack()
    {
        StartCoroutine(AttackCooldown());

        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation).GetComponent<Bullet>();
        
        // set bullet references
        bullet.playerTransform = transform;
        bullet.chunkRange = EntityManager.instance.chunkRange;
        
        // Set bullet stats
        bullet.moveSpeed = bulletSpeed;
        bullet.attackDamage = attackDamage;
        bullet.targetTag = targetTag;

        // Set bullet upgrades
        bullet.Setup(bulletUpgrades);
        bullet.heatSeekingBulletDetectionRange = heatSeekingBulletDetectionRange;

        EntityManager.instance.entitys.Add(bullet);

        // Hand animation
        int rnd = Random.Range(0, 2);
        handAnim.SetFloat("Rnd", rnd);
        handAnim.SetTrigger("Attack");
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void Rotate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - (Vector2)handParent.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handParent.rotation = Quaternion.Euler(0, 0, angle);
    }
}