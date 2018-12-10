using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public int score=0;
    public float LengthToScorePoint = 0.1f;
    private float lengthDone=0f;
    private float prevPosX = 0;
	// Use this for initialization
	void Start () {
        prevPosX = gameObject.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        lengthDone += gameObject.transform.position.x - prevPosX;
        prevPosX = gameObject.transform.position.x;

        if (lengthDone>LengthToScorePoint)
        {
            int delta = Mathf.FloorToInt(lengthDone / LengthToScorePoint);
            score += delta;
            lengthDone -= LengthToScorePoint * delta;
        }

    }
}
