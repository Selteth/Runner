using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    public void Deactivate()
    {
        Destroy(this);
    }
}