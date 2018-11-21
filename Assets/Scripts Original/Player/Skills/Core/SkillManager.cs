using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    // Player skills
    private readonly IList<ISkill> skills = new List<ISkill>();

    // Debug only
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateSkill(2);
    }
    // End debug only

    // Adds new skill of specified type
    public void AddSkill<SkillType>() where SkillType : Component
    {
        gameObject.AddComponent<SkillType>();
        skills.Add(GetComponent<SkillType>() as ISkill);

        // Then show new skill in UI...
    }

    // Activates skill with specified index
    public void ActivateSkill(int index)
    {
        if (index >= 0 && index < skills.Count)
        {
            ISkill skill = skills[index];
            if (skill.CanActivate())
                skill.Activate();
            // Debug only
            else
                Debug.Log("On cooldown");
            // End debug only
        }
    }
}
