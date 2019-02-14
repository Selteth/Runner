﻿using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float maxJumpTime;
    public float waitStartTime;
    
    private Transform jumpCheck;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private float jumpTimeCounter = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool shouldJump = false;

    void Awake()
    {
        DisableMoving();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCheck = transform.Find("JumpCheck");
        StartCoroutine("WaitBeforeStart");
    }

    private bool testVar = false;

    void Update()
    {
        HandleVerticalInput();
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("runSpeed", runSpeed);

        {
            if (!testVar)
            {
                Debug.Log("Debug block and variable in Movement.Update()");
                Debug.Log("Press 1 for GhostSkill");
                Debug.Log("Press 2 for FlySkill");
                Debug.Log("Press 3 for HighJumpSkill");

                Debug.LogError("Fix bug with platform generation when using HighJumpSkill");

                testVar = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ISkill skill = gameObject.AddComponent<GhostSkill>();
                skill.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ISkill skill = gameObject.AddComponent<FlySkill>();
                skill.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ISkill skill = gameObject.AddComponent<HighJumpSkill>();
                skill.Activate();
            }
        }
    }


    void FixedUpdate()
    {
        Jump();
        playerRigidbody.velocity = new Vector2(runSpeed, playerRigidbody.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, jumpCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public bool IsFalling()
    {
        return !isGrounded && playerRigidbody.velocity.y < 0;
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

        if (isJumping && shouldJump && jumpTimeCounter <= maxJumpTime)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            jumpTimeCounter += Time.fixedDeltaTime;
        }
    }

}
