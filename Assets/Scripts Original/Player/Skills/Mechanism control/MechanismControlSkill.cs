using UnityEngine;

public class MechanismControlSkill : Skill
{
    void Awake()
    {
        cooldown = 5f;
    }

    protected override void DoActivate()
    {
        // Debug only
        Debug.Log("MechanismControlSkill activated");
        // End debug only

        // TODO. Change mouse cursor to some special
    }

    protected override void DoDeactivate()
    {
        // Debug only
        Debug.Log("MechanismControlSkill deactivated");
        // End debug only

        // TODO. Change mouse cursor back to normal
    }
}