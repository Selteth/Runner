using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMechanism : MonoBehaviour, MechanismInterface {
    public void Activate()
    {
        GameObject.Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
