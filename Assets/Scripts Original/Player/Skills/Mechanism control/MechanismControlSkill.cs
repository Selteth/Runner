﻿

public class MechanismControlSkill : Skill
{
    void Awake()
    {
        cooldown = 1f; // 1 seconds for debug only
    }

    public void ActivateMechanism()
    {
        Deactivate();
    }

    protected override void DoActivate()
    {
        state = SkillState.Casting;
        // TODO. Change mouse cursor to some special
    }

    protected override void DoDeactivate()
    {
        // TODO. Change mouse cursor back to normal
    }

    protected override void Interrupt()
    {
        DoDeactivate();
    }
}