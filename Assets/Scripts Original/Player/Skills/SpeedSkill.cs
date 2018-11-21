using UnityEngine;
using System.Collections;

public class SpeedSkill : Skill
{
    // Value to multiply force added to the player while moving by
    private readonly float speedMultiplier = 2f;
    // Skill duration
    private float duration;
    // Player movement script
    private MovementControl movement;

    void Awake()
    {
        cooldown = 5f;
        duration = 15f;
        movement = GetComponent<MovementControl>();
    }

    // Increases player speed
    protected override void DoActivate()
    {
        movement.ChangeSpeed(speedMultiplier);
        StartCoroutine("SlowDown");
    }

    // Decreases player speed
    protected override void DoDeactivate()
    {
        movement.ChangeSpeed(1.0f / speedMultiplier);
    }

    // Slows player back to his speed after skill is over
    private IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

}
