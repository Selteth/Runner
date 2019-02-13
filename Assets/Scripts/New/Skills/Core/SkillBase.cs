using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    void FixedUpdate()
    {
        if (DoDeactivate())
            enabled = false;
    }

    public void Activate()
    {
        DoActivate();
        enabled = true;
    }

    protected abstract void DoActivate();
    protected abstract bool DoDeactivate();
}