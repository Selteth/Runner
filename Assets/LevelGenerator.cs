using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    private GameObject MainCamera = null;
    private GameObject ground = null;

	// Use this for initialization
	void Start () {
        foreach (GameObject go in gameObject.scene.GetRootGameObjects())
        {
            if (go.tag == "MainCamera")
                MainCamera = go;
            if (go.tag == "ground")
                ground = go;
        }
	}
	
	// Update is called once per frame
	void Update () {
		//if (MainCamera.GetComponent<Camera>().rect.xMax>ground.transform.)
	}
}
