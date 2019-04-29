using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public void ActivateGhostSkill()
    {
        ISkill skill = gameObject.GetComponent<GhostSkill>();
        if (skill == null)
            skill = gameObject.AddComponent<GhostSkill>();
    }

    public void ActivateFlySkill()
    {
        ISkill skill = gameObject.GetComponent<FlySkill>();
        if (skill == null)
            skill = gameObject.AddComponent<FlySkill>();
    }

    public void ActivateJumpSkill()
    {
        ISkill skill = gameObject.GetComponent<HighJumpSkill>();
        if (skill == null)
            skill = gameObject.AddComponent<HighJumpSkill>();
    }

}
