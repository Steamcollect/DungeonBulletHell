using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class LaserGolem : MonoBehaviour
{
    public Transform playerTransform;
    [HideInInspector] public float chunkRange;

    public string targetTag;
    public LayerMask targetLayer;

    [Header("Movement references")]
    public float moveSpeed;
    public float rotationSpeed;

    public float targetDetectionRange;
    public float movePosRadius;

    [Header("Comabt references")]
    public int attackDamage;
    public float attackTime;
    public float attackCooldown;

    bool canAttack = true;
    bool isPaused = false;

    Vector2 movePos;
    [SerializeField] Transform endLaser;
    public GameObject laserGO;

    Vector2 lookDir;
    float angle;

    Transform target;
    Vector2 velocity = Vector2.zero;

    public void Setup()
    {       
        transform.position = playerTransform.position;
        StartCoroutine(SetMovePos());
    }

    public void Update()
    {
        if (isPaused) return;

        if (target == null) target = GetAttackTarget();
        else if (canAttack) StartCoroutine(Attack());

        Move();
        Rotate();
    }

    void Move()
    {
        transform.position = Vector2.SmoothDamp(transform.position, (Vector2)playerTransform.position + movePos, ref velocity, moveSpeed);
    }

    IEnumerator SetMovePos()
    {
        Vector2 tmp = Random.insideUnitCircle.normalized * movePosRadius;
        movePos = tmp;

        float speed = moveSpeed;
        moveSpeed = speed * 3;
        yield return new WaitForSeconds(1.5f);
        moveSpeed = speed;
        
        yield return new WaitForSeconds(Random.Range(4, 10));
        StartCoroutine(SetMovePos());
    }
    
    Transform GetAttackTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(playerTransform.position, targetDetectionRange, targetLayer);
        
        Transform tmp = null;
        float minDist = 999;
        for (int i = 0; i < targets.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, targets[i].transform.position);

            if (dist <= targetDetectionRange && dist <= minDist) tmp = targets[i].transform;
        }
        return tmp;
    }

    IEnumerator Attack()
    {
        laserGO.SetActive(true);
        canAttack = false;
        float count = 0;

        while(count <= attackTime && !isPaused)
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, endLaser.position, targetLayer);

            foreach (RaycastHit2D hit in hits)
            {
                hit.transform.GetComponent<EntityHealth>().TakeDamage(attackDamage, transform.position);
            }

            yield return new WaitForSeconds(.2f);
            count += .2f;
        }

        laserGO.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void Rotate()
    {
        if (target != null) lookDir = target.position - transform.position;
        else lookDir = transform.position - playerTransform.position;

        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        angle = Mathf.LerpAngle(angle, targetAngle, target == null ? rotationSpeed : rotationSpeed / 3);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerTransform.position, movePosRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, targetDetectionRange);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, endLaser.position);
    }

    void OnPause()
    {
        isPaused = true;
    }
    void OnResume()
    {
        isPaused = false;
    }

    private void OnEnable()
    {
        GameStateManager.OnPaused += OnPause;
        GameStateManager.OnGameplay += OnResume;
    }
    private void OnDisable()
    {
        GameStateManager.OnPaused -= OnPause;
        GameStateManager.OnGameplay -= OnResume;
    }
}