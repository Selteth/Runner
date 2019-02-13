using UnityEngine;
using System.Collections;

public class FlySkill : SkillBase
{
    // Count of seconds that player can be in the air
    private readonly float duration = 1.5f;
    // Whether the ability has been already used
    private bool abilityUsed = false;
    // Whether the player is in the air and is holding jump button
    private bool isFlying = false;
    private Movement movement;
    private Rigidbody2D playerRigidbody;

    void Awake()
    {
        movement = GetComponent<Movement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!movement.IsGrounded() && Input.GetButtonDown("Jump")
            && !abilityUsed)
        {
            isFlying = true;
            StartCoroutine(TimeOver());
        }
        if (isFlying)
        {
            if (Input.GetButton("Jump"))
            {
                if (!abilityUsed)
                    Fly();
            }
            else
                StopFlying();
        }
    }

    protected override void DoActivate()
    {
        // Empty
    }

    protected override bool DoDeactivate()
    {
        return abilityUsed;
    }

    // Freezes Y-coordinate of the player
    private void Fly()
    {
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY
        | RigidbodyConstraints2D.FreezeRotation;
    }

    // Releases Y-coordinate of the player
    private void StopFlying()
    {
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        abilityUsed = true;
    }

    // Disables ability after N seconds
    private IEnumerator TimeOver()
    {
        yield return new WaitForSeconds(duration);
        StopFlying();
    }

}