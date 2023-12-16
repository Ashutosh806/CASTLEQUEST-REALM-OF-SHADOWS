using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioClip jump;

    private float dirX = 0f;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator anim;
    private enum MovementState { idle, running, jumping, falling }
    private PlayerBlock playerBlock;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        playerBlock = GetComponent<PlayerBlock>();
    }

    void Update()
    {
        if (playerBlock != null && playerBlock.IsInBlockState())
        {
            return;
        }
        dirX = Input.GetAxis("Horizontal");

        if (dirX > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SoundManager.instance.PlaySound(jump);
        }
        
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        else if (dirX != 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}