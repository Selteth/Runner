using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScoreTable : MonoBehaviour {
    private string text;
    private Text textComp;

    private ScoreTableLogic stl;

    void Start()
    {
        textComp = gameObject.GetComponent<Text>();
        text = textComp.text;

        foreach (GameObject go in gameObject.scene.GetRootGameObjects())
        {
            stl = go.GetComponent<ScoreTableLogic>();//)
            if (stl != null)
                break;
        }


        foreach (int score in stl.GetResults())
            text += "("+score+")" + "\n";
        textComp.text = text;//.GetEnumerator().Current;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
