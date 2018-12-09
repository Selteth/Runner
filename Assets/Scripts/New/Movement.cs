using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Horizontal movement speed
    public float runSpeed;
    // Vertical movement speed
    public float jumpSpeed;
    // Maximum time the player can be rise up when jumping
    public float maxJumpTime;

    private Rigidbody2D playerRigidbody;
    private float distanceToGround;
    private float jumpTimeCounter = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool shouldJump = false;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
    }

    private void Start()
    {
        playerRigidbody.velocity = new Vector2(runSpeed, 0);
    }

    private void Update()
    {
        HandleVerticalInput();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void HandleVerticalInput()
    {
        if (Input.GetButton("Jump"))
        {
            shouldJump = true;
        }
        else
        {
            shouldJump = false;
            isJumping = false;
        }

        if (IsGrounded())
        {
            isGrounded = true;
            isJumping = false;
            jumpTimeCounter = 0f;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (shouldJump && isGrounded)
        {
            Debug.Log("Here");
            isJumping = true;
            isGrounded = false;
        }

        if (isJumping && shouldJump && jumpTimeCounter < maxJumpTime)
        {
            Debug.Log("And here");
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            jumpTimeCounter += Time.fixedDeltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceToGround + 0.1f, 1 << LayerMask.NameToLayer("Ground"));
    }

}
