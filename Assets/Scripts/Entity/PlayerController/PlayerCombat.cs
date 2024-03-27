using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Statistics references")]
    public float attackCooldown;
    public float bulletSpeedModifier;
    public int attackDamageModifier;

    bool canAttack = true;

    public string targetTag;

    [Header("Combat references")]
    public GameObject bulletPrefab;

    public Transform handParent;
    public Transform attackPoint;

    public Animator handAnim;

    float angle;

    Vector2 lookDir;
    Vector2 mousePos;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canAttack) Attack();

        Rotate();
    }

    void Attack()
    {
        StartCoroutine(AttackCooldown());

        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation).GetComponent<Bullet>();
        bullet.moveSpeed *= bulletSpeedModifier;
        bullet.attackDamage *= attackDamageModifier;
        bullet.targetTag = targetTag;

        EntityManager.instance.bullets.Add(bullet);

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