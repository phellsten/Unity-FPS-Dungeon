// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour {
	public Button playButton;
	public Button quitButton;

	void Start () {
		playButton.onClick.AddListener (PlayGame);
		quitButton.onClick.AddListener (QuitGame);
	}
	
	void PlayGame() {
		SceneManager.LoadScene ("level1");
	}

	void QuitGame() {
		Application.Quit();
	}
}
