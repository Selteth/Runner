using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float maxJumpTime;
    public float waitStartTime;

    private Transform jumpCheck;
    private Rigidbody2D playerRigidbody;
    private float jumpTimeCounter = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool shouldJump = false;

    private void Awake()
    {
        DisableMoving();
        playerRigidbody = GetComponent<Rigidbody2D>();
        jumpCheck = transform.Find("JumpCheck");
        StartCoroutine("WaitBeforeStart");
    }

    private void Start()
    {

    }
    
    private void Update()
    {
        HandleVerticalInput();
    }

    private void FixedUpdate()
    {
        Jump();
        playerRigidbody.velocity = new Vector2(runSpeed, playerRigidbody.velocity.y);
    }

    public bool IsFalling()
    {
        return !isGrounded && playerRigidbody.velocity.y < 0;
    }

    public void ChangeRunSpeed(float multiplier)
    {
        runSpeed *= multiplier;
    }

    public void ChangeJumpTime(float multiplier)
    {
        maxJumpTime *= multiplier;
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(waitStartTime);
        EnableMoving();
    }

    private void DisableMoving()
    {
        enabled = false;
    }

    private void EnableMoving()
    {
        enabled = true;
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
            isJumping = true;
            isGrounded = false;
        }

        if (isJumping && shouldJump && jumpTimeCounter < maxJumpTime)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            jumpTimeCounter += Time.fixedDeltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, jumpCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
    }

}
