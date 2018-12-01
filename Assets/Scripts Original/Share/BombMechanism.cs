using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMechanism : MonoBehaviour, MechanismInterface {
    private static GameObject explObj = null;
    public void Awake()
    {
        if(explObj == null)
        explObj = Resources.Load<GameObject>("Prefabs/Explosion");
    }

    public void Activate()
    {
        GameObject.Instantiate(explObj, gameObject.transform.position, Quaternion.Euler(0,0,0));
        GameObject.Destroy(gameObject);
    }

}
