using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    public void Activate()
    {
        enabled = true;
    }

    protected void Deactivate()
    {
        enabled = false;
    }
}