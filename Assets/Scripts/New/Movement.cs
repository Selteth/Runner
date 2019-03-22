using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float maxJumpTime;
    public float waitStartTime;

    private Variables variables;
    private Transform jumpCheck;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool shouldJump = false;
    private bool canFly = false;

    private int groundLayer;
    private int obstacleGroundLayer;

    void Awake()
    {
        DisableMoving();
        variables = GameObject.Find("Variables").GetComponent<Variables>();
        variables.playerRunSpeed = runSpeed;
        variables.playerJumpSpeed = jumpSpeed;
        variables.playerJumpTime = maxJumpTime;
        playerCollider = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCheck = transform.Find("JumpCheck");
        
        groundLayer = LayerMask.NameToLayer("Ground");
        obstacleGroundLayer = LayerMask.NameToLayer("ObstacleGround");

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

                testVar = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ISkill skill = gameObject.GetComponent<GhostSkill>();
                if (skill == null)
                    skill = gameObject.AddComponent<GhostSkill>();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ISkill skill = gameObject.GetComponent<FlySkill>();
                if (skill == null)
                    skill = gameObject.AddComponent<FlySkill>();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ISkill skill = gameObject.GetComponent<HighJumpSkill>();
                if (skill == null)
                    skill = gameObject.AddComponent<HighJumpSkill>();
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
        Vector2 start = new Vector2(transform.position.x + playerCollider.bounds.extents.x - 0.1f,
            transform.position.y - playerCollider.bounds.extents.y);
        Vector2 end = jumpCheck.position;
        
        bool isOnGround = Physics2D.Linecast(start, end, 1 << groundLayer);
        bool isOnObstacle = Physics2D.Linecast(start, end, 1 << obstacleGroundLayer)
            && Physics2D.GetIgnoreLayerCollision(gameObject.layer, obstacleGroundLayer) == false;

        return isOnGround || isOnObstacle;
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
            canFly = true;
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
            StartCoroutine(StopFlying());
        }

        if (isJumping && shouldJump && canFly)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
        }
    }

    private IEnumerator StopFlying()
    {
        yield return new WaitForSeconds(maxJumpTime);
        canFly = false;
    }

}
