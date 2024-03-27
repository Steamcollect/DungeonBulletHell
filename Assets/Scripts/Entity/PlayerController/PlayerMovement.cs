using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

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

    private void Update()
    {
        GetInput();

        SetAnimation();
    }

    private void FixedUpdate()
    {
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
}