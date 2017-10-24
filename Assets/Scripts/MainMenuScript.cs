// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {
	public Button playButton;
	public Button quitButton;
	public Button controls;


	void Start () {
		playButton.onClick.AddListener (PlayGame);
		quitButton.onClick.AddListener (QuitGame);
		controls.onClick.AddListener (Controls);
	}
	
	void PlayGame() {
		SceneManager.LoadScene ("level1");
	}

	void QuitGame() {
		Application.Quit();
	}

	void Controls() {
		SceneManager.LoadScene ("ControlsMenu");

	}

}
