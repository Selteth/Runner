using System.Collections;
using UnityEngine;

// Represents player skill base behaviour
public abstract class Skill : MonoBehaviour, ISkill {
    
    // Time during which player cannot activate the skill
    protected float cooldown;
    // Whether the skill is active
    protected bool isActive = false;
    // Whether the skill is on cooldown
    protected bool isOnCooldown = false;

    // Activates the skill
    public void Activate()
    {
        enabled = true;
        isActive = true;
        DoActivate();
    }

    // Returns true if skill can be activated at the moment
    public bool CanActivate()
    {
        return !isActive && !isOnCooldown;
    }
    
    // Activate implementation
    protected abstract void DoActivate();

    // Deactivates the skill
    public void Deactivate()
    {
        DoDeactivate();
        Cooldown();

        isActive = false;
        enabled = false;
    }

    // Deactivate implementation
    protected abstract void DoDeactivate();

    // Sets the skill on colldown
    protected void Cooldown()
    {
        isOnCooldown = true;
        StartCoroutine("WaitForCooldown");
    }
    
    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}