using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour {

    public Button PlayGameButton, ExitButton;

	// Use this for initialization
	void Start () {
        PlayGameButton.onClick.AddListener(PlayGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        SceneManager.LoadScene("SpriteLevel");
    }
    public void ExitGame()
    {
        
    }
}
