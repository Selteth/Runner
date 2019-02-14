using UnityEngine;

public class HighJumpSkill : SkillBase
{
    private enum JumpState
    {
        None, Ready, Active
    }

    private readonly float jumpMultiplier = 2.0f;
    private Movement playerMovement;
    private JumpState state;

    void Awake()
    {
        playerMovement = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        if (state == JumpState.None && playerMovement.IsGrounded())
        {
            playerMovement.jumpSpeed *= jumpMultiplier;
            state = JumpState.Ready;
        }
        else if (state == JumpState.Ready && !playerMovement.IsGrounded())
            state = JumpState.Active;
        else if (state == JumpState.Active && playerMovement.IsGrounded())
        {
            playerMovement.jumpSpeed /= jumpMultiplier;
            Deactivate();
        }
    }
    
}