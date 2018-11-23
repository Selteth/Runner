using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    [Header("Player characteristics")]
    public int startHP = 1;
    public int curHP;

    private Vector3 startPoint;

    public void Awake()
    {
        curHP = startHP;

        startPoint = transform.TransformPoint(gameObject.transform.position);
        // Debug only
        //GetComponent<SkillManager>().AddSkill<TeleportationSkill>();
        //GetComponent<SkillManager>().AddSkill<SpeedSkill>();
        //GetComponent<SkillManager>().AddSkill<MechanismControlSkill>();
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
        if (gameObject.tag.ToString() == "Player")
            Respawn();
        else if (gameObject.tag.ToString() == "Enemy")
            GameObject.Destroy(gameObject);
        //GameObject.Destroy(gameObject);
        //GameObject.Destroy();
    }

    public void Respawn()
    {
        gameObject.transform.Translate(transform.InverseTransformPoint(startPoint));
    }
}
