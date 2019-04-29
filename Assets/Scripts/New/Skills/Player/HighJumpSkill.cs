using UnityEngine;

public class HighJumpSkill : SkillBase
{
    private enum JumpState
    {
        None, Ready, Active
    }

    private Variables variables;
    private Movement playerMovement;
    private JumpState state;
    
    void Awake()
    {
        playerMovement = GetComponent<Movement>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();
    }

    void FixedUpdate()
    {
        if (state == JumpState.None && playerMovement.IsGrounded())
        {
            playerMovement.jumpSpeed *= variables.highJumpMultiplier;
            state = JumpState.Ready;
        }
        else if (state == JumpState.Ready && !playerMovement.IsGrounded())
            state = JumpState.Active;
        else if (state == JumpState.Active && playerMovement.IsGrounded())
        {
            playerMovement.jumpSpeed /= variables.highJumpMultiplier;
            Deactivate();
        }
    }
    
}