using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [Header("Horizontal movement")]
    // Amount of force added to move the player left and right
    public float moveSpeed = 10f;
    // Horizontal movement axis
    private float horizontalAxis;

    [Header("Vertical movement")]
    // Amount of force added to make the player jump
    public float jumpSpeed = 12f;
    // Position on the player sprite that defines if he is on the ground
    private Transform groundCheck;
    // Whether or not the player is on the ground
    private bool isGrounded = false;
    // Whether the player should jump
    private bool shouldJump = false;

    // Used new to disable warning about hiding base member
    private new Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }
    
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        shouldJump = Input.GetButton("Jump");
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    void FixedUpdate()
    {
        MoveHorizontal();
        MoveVertical();
    }

    // Handles horizontal movement
    private void MoveHorizontal()
    {
        if (horizontalAxis * rigidbody.velocity.x < moveSpeed)
            rigidbody.velocity = new Vector2(moveSpeed * horizontalAxis, rigidbody.velocity.y);
    }

    // Handles vertical movement (jumping)
    private void MoveVertical()
    {
        if (shouldJump && isGrounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            isGrounded = false;
        }
    }

}
