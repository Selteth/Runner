using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {

    [Header("Player characteristics")]
    public int startHP = 1;
    public int curHP;

    public void Awake()
    {
        curHP = startHP;

        // Debug only
        GetComponent<SkillManager>().AddSkill<TeleportationSkill>();
        GetComponent<SkillManager>().AddSkill<SpeedSkill>();
        // End debug only
    }

    public void Damaged()
    {
        curHP -= 1;
        if (curHP < 1)
            Die();
    }

    public void Die()
    {
        GameObject.Destroy(gameObject);
        //GameObject.Destroy();
    }
}
