using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    bool isFacingRight = true;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public UnityEngine.Vector2 groundCheckSize = new UnityEngine.Vector2(0.49f, 0.03f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public UnityEngine.Vector2 wallCheckSize = new UnityEngine.Vector2(0.49f, 0.03f);
    public LayerMask wallLayer;

    [Header("WallMovement")]
    // Wall Sliding
    public float wallSlideSpeed = 2;
    bool isWallSliding;

    // Wall Jumping
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {   
        GroundCheck();
        Gravity();
        ProcessWallSlide();
        ProcessWallJump();

        if (!isWallJumping)
        {
            rb.velocity = new UnityEngine.Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
            Flip();
        }
    }

    [System.Obsolete]
    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            // player falls increasingly faster with time. doesn't feel as floaty.
            rb.gravityScale = baseGravity * fallSpeedMultiplier;

            // caps at max fall speed, so player doesn't fall too fast.
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }

        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<UnityEngine.Vector2>().x;
    }

    [System.Obsolete]
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            // default jump code
            if(context.performed)
            {
                // if holding down jump button, get full height
                rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;
            }

            else if (context.canceled)
            {
                // if doing light tap on jump button, get partial height
                rb.velocity = new UnityEngine.Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
            }
        }

        // wall jump code
        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); // jump away from wall
            wallJumpTimer = 0;

            // force flipping when jumping (more intuitive and comfortable for players)
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
            // wall jump lasts 0.5 sec, next wall jump in 0.6 sec.
        }

    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    [System.Obsolete]
    private void ProcessWallSlide()
    {
        // not grounded & on a wall. movement != 0.
        if (!isGrounded && WallCheck() && horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }

        else
        {
            isWallSliding = false;
        }
    }

    [System.Obsolete]
    private void ProcessWallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if ((isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
