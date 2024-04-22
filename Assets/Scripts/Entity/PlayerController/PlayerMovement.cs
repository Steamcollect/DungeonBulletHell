using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    bool isPaused = false;

    Vector2 moveInput;

    SpriteRenderer graphics;
    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        graphics = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isPaused) return;

        GetInput();

        SetAnimation();
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        Move();
    }

    void GetInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }
    void Move()
    {
        rb.velocity = moveInput * moveSpeed * Time.deltaTime;
    }

    void SetAnimation()
    {
        anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        if (rb.velocity.x < -.1) graphics.flipX = true;
        else if (rb.velocity.x > .1) graphics.flipX = false;
    }

    void OnPause()
    {
        rb.velocity = Vector2.zero;
        anim.speed = 0;
        isPaused = true;
    }
    void OnResume()
    {
        anim.speed = 1;
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