using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour {
	public Button playButton;
	public Button quitButton;

	// Use this for initialization
	void Start () {
		playButton.onClick.AddListener (PlayGame);
		quitButton.onClick.AddListener (QuitGame);

	}
	
	void PlayGame() {
		SceneManager.LoadScene ("main");
	}

	void QuitGame() {
		Application.Quit();
	}
}
