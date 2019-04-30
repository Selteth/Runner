using UnityEngine;
using System.Collections;

public class FlySkill : SkillBase
{
    // Whether the ability has been already used
    private bool abilityUsed = false;
    // Whether the player is in the air and is holding jump button
    //private bool isFlying = false;
    private Movement movement;
    private Rigidbody2D playerRigidbody;
    private Variables variables;

    void Awake()
    {
        movement = GetComponent<Movement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();
    }

    void Update()
    {


        if (//!movement.IsGrounded()&& 
            !abilityUsed)
        {
            
            Fly();
            StartCoroutine(TimeOver());
        }

        if (Input.GetButtonDown("Jump"))
            StopFlying();

        if (abilityUsed)
            Deactivate();
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
        yield return new WaitForSeconds(variables.flySkillDuration);
        StopFlying();
    }

}